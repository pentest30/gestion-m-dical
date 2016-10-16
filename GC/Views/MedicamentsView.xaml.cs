using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Bars;
using Gc.Core.Data;
using Gc.Core.DataManage.Core;
using GC.Views.Parametres;

namespace GC.Views
{
    /// <summary>
    /// Interaction logic for MedicamentsView.xaml
    /// </summary>
    public partial class MedicamentsView
    {
        private readonly IRepository<Medicament> _repository;
        readonly MainWindow _mainWindow = (MainWindow)Application.Current.MainWindow;

        public MedicamentsView( )
        {
           
            _mainWindow.LoadingDecorator.IsSplashScreenShown = true;
            InitializeComponent();
             _repository = _mainWindow.AutofacBootStrap.ResoleMedicamentRepo();
            PopulateCbMedicament();
          
            _mainWindow.LoadingDecorator.IsSplashScreenShown = false;
        }

     
        private void EditBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var item = GridControl.SelectedItem as Medicament;
            if (item == null)
            {
                MessageBox.Show("Séléctionner un champ", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var frm = new AddUpdateMedicamentWin(_repository, item);
            frm.UpdateDataDg += PopulateCbMedicament;
            frm.Show();
        }
    

        private void TxtSearch_OnTextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private void ImportBtn_OnClick(object sender, RoutedEventArgs e)
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                /* run your code here */
                var b = ImportFromXls.ImporteListe(@"E:\PFE projects\GestionMedicale\Gm.UI\App_Data\uploads\PRODUIT.mdb");
                MessageBox.Show("Enregistrement terminer");
            }).Start();
           
        }

        private void BarItem_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var frm = new AddUpdateMedicamentWin(_repository);
            frm.UpdateDataDg += PopulateCbMedicament;
            frm.Show();
        }

        private void PopulateCbMedicament()
        {
            string[] param = { "Dci" };
            GridControl.ItemsSource = _repository.QueryWith(param).ToList();
        }
    }
}
