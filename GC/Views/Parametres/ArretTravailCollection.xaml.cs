using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using Gc.Core.Data;
using Gc.Core.DataManage.Core;


namespace GC.Views.Parametres
{
    /// <summary>
    /// Interaction logic for ArretTravailCollection.xaml
    /// </summary>
    public partial class ArretTravailCollection
    {
        IRepository<DossierPatient> _repositoryPatient;
        readonly MainWindow _mainWindow = (MainWindow)Application.Current.MainWindow;
        IRepository<ArretTravail> _repository; 
        public ArretTravailCollection()
        {
            InitializeComponent();
            _mainWindow.LoadingDecorator.IsSplashScreenShown = true;
            _repositoryPatient = _mainWindow.AutofacBootStrap.ResoleDossierPatientRepo();
            _repository = _mainWindow.AutofacBootStrap.ArretTravailRepo();

            GridControl.ItemsSource = _repositoryPatient.Query().ToList();
            _mainWindow.LoadingDecorator.IsSplashScreenShown = false;}

        private void TxtSearch_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void GridControl_OnSelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
        {
            
        }

        private void AddItem_OnItemClick(object sender, ItemClickEventArgs e)
        {
            
        }

        private void EditCommand_OnItemClick(object sender, ItemClickEventArgs e)
        {
            
        }

        private void DeleteCommand_OnItemClick(object sender, ItemClickEventArgs e)
        {
            
        }

        private void edit_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            
        }

        private void DataGrid_OnSelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
        {
            
        }
    }
}
