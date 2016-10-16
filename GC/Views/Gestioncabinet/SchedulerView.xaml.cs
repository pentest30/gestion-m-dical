using System.Linq;
using System.Windows;
using GC.MV;

namespace GC.Views.Gestioncabinet
{
    /// <summary>
    /// Interaction logic for SchedulerView.xaml
    /// </summary>
    public partial class SchedulerView {
        readonly MainWindow _mainWindow = (MainWindow)Application.Current.MainWindow;

        public SchedulerView()
        {
            _mainWindow.LoadingDecorator.IsSplashScreenShown = true;
            InitializeComponent();
            var repositoryRendezvous = _mainWindow.AutofacBootStrap.RepositoryRendezVous();
            string[] parm = { "Patient" };
            var cl = new RendeVousCollectionView(repositoryRendezvous.QueryWith(parm).ToList());
            scheduler.Storage.AppointmentStorage.DataSource = cl.Rendezvous;
            _mainWindow.LoadingDecorator.IsSplashScreenShown = false;
        }
    }
}
