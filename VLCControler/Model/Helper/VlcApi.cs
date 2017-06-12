using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DerAtrox.VLCController.Exceptions;
using DerAtrox.VLCController.Model.Api;
using Newtonsoft.Json;

namespace DerAtrox.VLCController.Model.Helper
{
    public class VlcApi
    {
        private readonly LoginCredentials _cred;
        private readonly string _baseUrl;


        public VlcApi(LoginCredentials cred)
        {
            _cred = cred;
            _baseUrl = "http://" + _cred.Hostname + ":" + _cred.Port + "/requests/";
        }

        public async Task<Status> GetStatus(string action = "")
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

    }
}
