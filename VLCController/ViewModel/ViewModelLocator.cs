namespace DerAtrox.VLCController.ViewModel
{
    public class ViewModelLocator
    {
        private static MainViewModel _main;

        public ViewModelLocator()
        {
            _main = new MainViewModel();
        }

        public MainViewModel Main
        {
            get
            {
                return _main;
            }
        }

    }
}
