using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Bars;
using Gc.Core.Data;
using Gc.Core.DataManage.Core;


namespace GC.Views.Parametres
{
    /// <summary>
    /// Interaction logic for SymptomesCollection.xaml
    /// </summary>
    public partial class SymptomesCollection
    {
        readonly MainWindow _mainWindow = (MainWindow)Application.Current.MainWindow;
        readonly IRepository<Symptome> _repository;
        public SymptomesCollection()
        {
            InitializeComponent();
            _repository = _mainWindow.AutofacBootStrap.SymptomeRepo();
            UpdateDataGrid();
        }

        private void AddItem_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var frm = new AddEditExamenClinique(_repository, new Symptome());
            frm.UpdateDataDg += UpdateDataGrid;
            frm.Show();
        }

        private void UpdateDataGrid()
        {
            gridControl.ItemsSource = _repository.Query().ToList();
        }

        private void EditCommand_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = gridControl.SelectedItem as Symptome;
            if (item == null)
            {
                MessageBox.Show("Attention", "Séléctionner un champ !!");
                return;
            }
            var frm = new AddEditExamenClinique(_repository,item);
            frm.UpdateDataDg += UpdateDataGrid;
            frm.Show();
        }

        private void DeleteCommand_OnItemClick(object sender, ItemClickEventArgs e)
        {
            
        }

        private void TxtSearch_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}
