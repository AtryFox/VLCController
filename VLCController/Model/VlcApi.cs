using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using DerAtrox.VLCController.Exceptions;
using DerAtrox.VLCController.Model.Api;
using Newtonsoft.Json;

namespace DerAtrox.VLCController.Model
{
    public class VlcApi
    {
        private readonly LoginCredentials _cred;
        private readonly string _baseUrl;

        private readonly ObservableCollection<string> _statusRequestStack = new ObservableCollection<string>();

        private readonly BackgroundWorker _statusRequestWorker = new BackgroundWorker() {WorkerSupportsCancellation = true};

        private readonly Timer _statusUpdater = new Timer() { Interval = 1000 };

        public event EventHandler<Status> StatusChanged;
        public event EventHandler<Exception> RequestError;
        public event EventHandler<int> StatusRequestCountChanged;

        public VlcApi(LoginCredentials cred)
        {
            _cred = cred;
            _baseUrl = "http://" + _cred.Hostname + ":" + _cred.Port + "/requests/";

            _statusRequestWorker.DoWork += (sender, args) =>
            {
                _statusUpdater.Stop();

                while (_statusRequestStack.Count >= 1)
                {
                    if (_statusRequestWorker.CancellationPending) return;

                    try
                    {
                        Status status = GetStatusSync(_statusRequestStack[0]);
                        if (_statusRequestStack[0] == "") StatusChanged?.Invoke(this, status);
                        _statusRequestStack.RemoveAt(0);
                    }
                    catch (ApiRespondException e)
                    {
                        RequestError?.Invoke(this, e);
                        _statusRequestStack.Clear();
                        return;
                    }
                }
                
                _statusUpdater.Start();
            };

            _statusRequestStack.CollectionChanged += (sender, args) =>
            {
                if (!_statusRequestWorker.IsBusy) _statusRequestWorker.RunWorkerAsync();
                StatusRequestCountChanged?.Invoke(sender, _statusRequestStack.Count);
            };

            _statusUpdater.Elapsed += (sender, args) =>
            {
                RequestStatus();
            };

            _statusUpdater.Start();
        }

        public void RequestStatus(string action = "")
        {
            _statusRequestStack.Add(action);
        }

        private Status GetStatusSync(string action = "")
        {
            try
            {
                return GetStatus(action).Result;
            }
            catch (AggregateException e)
            {
                if (e.InnerException != null) throw e.InnerException;
                throw;
            }
        }

        private async Task<Status> GetStatus(string action = "")
        {
            string rawData;

            using (WebClient client = new WebClient())
            {
                string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(":" + _cred.Password));
                client.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;

                try
                {
                    rawData = await client.DownloadStringTaskAsync(_baseUrl + "status.json?command=" + action);
                }
                catch (Exception e)
                {
                    throw new ApiRespondException(e.Message);
                }

            }

            if (rawData == "")
            {
                throw new ApiRespondException("No data recieved!");
            }

            return JsonConvert.DeserializeObject<Status>(rawData);
        }

        public void Dispose() {
            _statusRequestStack.Clear();
            _statusRequestWorker.CancelAsync();
            _statusUpdater.Stop();
            StatusChanged = null;
            RequestError = null;
            StatusRequestCountChanged = null;
        }
    }
}
