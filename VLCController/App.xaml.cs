using System.Windows;
using MahApps.Metro;

namespace DerAtrox.VLCController
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.GetAccent("Orange"), ThemeManager.GetAppTheme("BaseDark"));

            base.OnStartup(e);
        }
    }
}
