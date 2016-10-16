using System;
using System.Windows;
using DevExpress.Xpf.Bars;
using Gc.Core;
using Gc.Core.Data;
using GC.DXSplashScreen;
using GC.Views;
using GC.Views.Gestioncabinet;
using GC.Views.Parametres;
using GC.Views.Statistics;

namespace GC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public  AutofacBootStrap AutofacBootStrap { get; set; }

        public MainWindow()
        {

            DevExpress.Xpf.Core.DXSplashScreen.Show<SplashScreenView1>();
           
            InitializeComponent();
           
            DevExpress.Xpf.Core.DXSplashScreen.Close();
            LoadingDecorator.IsSplashScreenShown = true;
            AutofacBootStrap = new AutofacBootStrap();
            var repo = AutofacBootStrap.RepositoryCabinet();
            if (repo.Query().Count == 0)
            {
                LoadingDecorator.IsSplashScreenShown = false;
                var frm = new CabinetView(repo);
                frm.ShowDialog();
               
            }
            else
            {
                LoadingDecorator.IsSplashScreenShown = false;
            }
            Loaded += MainWindow_OnLoaded;
             
        }

        private void PatientsItem_OnItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                ContentControl.Content = null;
                ContentControl.Content = new ListePatientsView {Name = "LP"};
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);//
            }
        }
        
        private void ConsultItem_OnItemClick(object sender, ItemClickEventArgs e)
        {
            ContentControl.Content = null;
            ContentControl.Content = new ConsultStatisticsView();

        }

        private void HomeItem_OnItemClick(object sender, ItemClickEventArgs e)
        {
            ContentControl.Content = null;
            ContentControl.Content = new MainView();
        }
        private void BarItem_OnItemClick(object sender, ItemClickEventArgs e)
        {
            ContentControl.Content = null;
            ContentControl.Content =  new MedicamentsView();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
          
            ContentControl.Content = new MainView();
           // this.Activate();
           
           
        }

        private void AddConsultationItem_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var frm = new AddConsultation(new Consultation());
            frm.Show();
        }

        private void RendezvousItem_OnItemClick(object sender, ItemClickEventArgs e)
        {
            ContentControl.Content = null;
            ContentControl.Content = new RendezVousView();
        }

        private void SchedulItem_OnItemClick(object sender, ItemClickEventArgs e)
        {
           var  frm= new SchedulerView();
            frm.Show();
        }

        private void NewRendezvousItem_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var frm =  new AddRendezVous(new Rendezvou());
            frm.Show();
        }

        private void AddpatientsItem_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var frm = new AddPatient(AutofacBootStrap.ResoleDossierPatientRepo());
            frm.Show();
        }


        private void AnalysesItem_OnItemClick(object sender, ItemClickEventArgs e)
        {
            ContentControl.Content = null;
            ContentControl.Content = new AnnalyseColletion();
        }

        private void SyptomeItem_OnItemClick(object sender, ItemClickEventArgs e)
        {
            ContentControl.Content = null;
            ContentControl.Content = new SymptomesCollection();}

        private void AntecedentsItem_OnItemClick_Content_(object sender, ItemClickEventArgs e)
        {
            ContentControl.Content = null;
            ContentControl.Content =  new AntecedentsCollection();}

        private void ArretsItem_OnItemClick(object sender, ItemClickEventArgs e)
        {
            ContentControl.Content = null;
            ContentControl.Content = new ArretTravailCollection();
        }

        private void CabinetItem_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var repo = AutofacBootStrap.RepositoryCabinet();
            //ContentControl.Content = null;
            var frm =  new CabinetView(repo);
            frm.Show();
        }
    }
}
