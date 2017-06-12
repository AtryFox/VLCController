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
        private string _currentTrack = "";
        private string _connectionString = "";
        private bool _connectionState = false;
        private string _hostname = "localhost";
        private string _password = "";
        private int _port = 8080;

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

        public string CurrentTrack
        {
            get
            {
                return _currentTrack;
            }
            set
            {
                _currentTrack = value;
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

        public RelayCommand Mute => new RelayCommand(() => MuteCommand());
        public RelayCommand TestConnection => new RelayCommand(() => TestConnectionCommand());


        public async void TestConnectionCommand()
        {
            VlcApiConnection = new VlcApi(new LoginCredentials(Hostname, Port, Password));

            try
            {
                Status data = await VlcApiConnection.GetStatus();
                CurrentTrack = data.Information.Category.Meta.Title;
            }
            catch (ApiRespondException e)
            {
                ConnectionString = "Connection Error";
                ConnectionState = false;
                return;
            }

            ConnectionString = "Connection successfully";
            ConnectionState = true;
        }

        public async void MuteCommand()
        {
            string action = "volume&val=";

            if (IsMute)
            {
                action += "50";
            }
            else
            {
                action += "0";
            }

            try
            {
                Status data = await VlcApiConnection.GetStatus(action);
            }
            catch (ApiRespondException e)
            {
                MessageBox.Show("Could not connect to remote host");
                return;
            }

            IsMute = !IsMute;

        }
    }
}
