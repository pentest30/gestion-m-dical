using System.Windows;
using DevExpress.Xpf.Core;

namespace GC
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        //public AutofacBootStrap AutofacBootStrap { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
           
        }

        private void OnAppStartup_UpdateThemeName(object sender, StartupEventArgs e)
        {
            DevExpress.Xpf.Core.ApplicationThemeHelper.UpdateApplicationThemeName();
          
        }
    }
}
