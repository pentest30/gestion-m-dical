using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using Gc.Core.Data;
using Gc.Core.DataManage.Core;

using GC.Views.Gestioncabinet;

namespace GC.Views
{
    /// <summary>
    /// Interaction logic for RendezVousView.xaml
    /// </summary>
    public partial class RendezVousView
    {
        readonly MainWindow _mainWindow = (MainWindow)Application.Current.MainWindow;
        readonly IRepository<Rendezvou> _repositoryRendezvous; 
        public RendezVousView()
        {
            _mainWindow.LoadingDecorator.IsSplashScreenShown = true;
            InitializeComponent();
            _repositoryRendezvous = _mainWindow.AutofacBootStrap.RepositoryRendezVous();
            DisplayDg();
            _mainWindow.LoadingDecorator.IsSplashScreenShown = false;
        }

        private void DisplayDg()
        {
            string[] parm = {"Patient"};
            DataGrid.ItemsSource = _repositoryRendezvous.QueryWith(parm).ToList();
        }


        private void EditCommand_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var otem = DataGrid.SelectedItem as Rendezvou;
            if (otem == null)
            {MessageBox.Show("Séléctionner un champ", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var frm = new AddRendezVous(otem);
            frm.UpdateDataDg += DisplayDg;
            frm.Show();
        }



        

     

        private void AddItem_OnItemClick(object sender, ItemClickEventArgs e)
        {
             var frm = new AddRendezVous(new Rendezvou());
            frm.UpdateDataDg += DisplayDg;
            frm.Show();
        }

        private void Edit_OnEditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            var dateTime = ((DateEdit)sender).DateTime;
            var selectedDate = dateTime.Date.ToString(CultureInfo.InvariantCulture);
            var nextday = dateTime.AddDays(1).Date.ToString(CultureInfo.CurrentUICulture);
            DataGrid.FilterString = string.Format("[Date] >=#{0}# and [Date]<#{1}#",
                selectedDate, nextday);
        }

        private void DeleteCommand_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var otem = DataGrid.SelectedItem as Rendezvou;
            if (otem == null)
            {
                MessageBox.Show("Séléctionner un champ", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var result = MessageBox.Show(" Est vous sur ?", "Attention", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result != MessageBoxResult.Yes) return;
            _repositoryRendezvous.Delete(otem.Id);
            _repositoryRendezvous.SaveChanges();
            DisplayDg();}

        private void DataGrid_OnSelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
        {
            
        }

        private void TxtSearch_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var s = TxtSearch.Text;
            string[] parm = { "Patient" };
            var  q=(from p in   _repositoryRendezvous.QueryWith(parm)
                    let nc =p.Patient.Nom + " " + p.Patient.Prenom
                    where  nc.Contains(s) 
                    select p).ToList();
            DataGrid.ItemsSource = q;}
    }
}
