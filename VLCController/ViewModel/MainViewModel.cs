using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using DerAtrox.VLCController.Exceptions;
using DerAtrox.VLCController.Model;
using DerAtrox.VLCController.Model.Api;

namespace DerAtrox.VLCController.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private bool _isMute = false;
        private string _connectionString = "";
        private bool _connectionState = false;
        private string _hostname = "localhost";
        private string _password = "";
        private int _port = 8080;
        private int _volume;
        private int _lastVolume;

        private int _statusRequestCount;

        private Status _status;

        public VlcApi VlcApiConnection;

        public bool IsMute
        {
            get
            {
                return _isMute;
            }
            set
            {
                _isMute = value;
                RaisePropertyChanged();
            }
        }

        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
            set
            {
                _connectionString = value;
                RaisePropertyChanged();
            }
        }

        public bool ConnectionState
        {
            get
            {
                return _connectionState;
            }
            set
            {
                _connectionState = value;
                RaisePropertyChanged();
            }
        }

        public string Hostname
        {
            get
            {
                return _hostname;
            }
            set
            {
                _hostname = value;
                RaisePropertyChanged();
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                RaisePropertyChanged();
            }
        }

        public int Port
        {
            get
            {
                return _port;
            }
            set
            {
                _port = value;
                RaisePropertyChanged();
            }
        }

        public Status Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;

                Volume = Status.Volume;

                RaisePropertyChanged();
            }
        }

        public int Volume
        {
            get
            {
                return _volume;
            }
            set
            {
                _volume = value;

                IsMute = Volume == 0;

                VlcApiConnection.RequestStatus("volume&val=" + Volume);

                RaisePropertyChanged();
            }
        }

        public int StatusRequestCount
        {
            get
            {
                return _statusRequestCount;
            }
            set
            {
                _statusRequestCount = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand Mute => new RelayCommand(() => MuteCommand());
        public RelayCommand TestConnection => new RelayCommand(() => TestConnectionCommand());

        public void TestConnectionCommand() {
            if (VlcApiConnection != null) VlcApiConnection.Dispose();
            GC.Collect();
            VlcApiConnection = new VlcApi(new LoginCredentials(Hostname, Port, Password));

            VlcApiConnection.StatusChanged += (sender, status) =>
            {
                Status = status;
                ConnectionString = "Connected";
                ConnectionState = true;
            };

            VlcApiConnection.RequestError += (sender, e) =>
            {
                MessageBox.Show(e.Message);
                ConnectionString = "Error connecting";
                ConnectionState = false;
            };

            VlcApiConnection.StatusRequestCountChanged += (sender, e) => {
                StatusRequestCount = e;
            };

            VlcApiConnection.RequestStatus();
        }

        public void MuteCommand()
        {
            if (IsMute)
            {
                Volume = _lastVolume;
            }
            else
            {
                _lastVolume = Volume;
                Volume = 0;
            }
        }
    }
}
