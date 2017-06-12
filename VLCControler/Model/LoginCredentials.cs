namespace DerAtrox.VLCController.Model
{
    public class LoginCredentials
    {
        public LoginCredentials(string hostname, int port, string password)
        {
            Hostname = hostname;
            Port = port;
            Password = password;
        }

        public string Hostname { get; private set; }
        public int Port { get; private set; }
        public string Password { get; private set; }
    }
}
