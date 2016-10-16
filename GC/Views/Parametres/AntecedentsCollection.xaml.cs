using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Bars;
using Gc.Core.Data;
using Gc.Core.DataManage.Core;


namespace GC.Views.Parametres
{
    /// <summary>
    /// Interaction logic for AntecedentsCollection.xaml
    /// </summary>
    public partial class AntecedentsCollection
    {
        readonly MainWindow _mainWindow = (MainWindow)Application.Current.MainWindow;
        readonly IRepository<Antecedent> _repository;
        public AntecedentsCollection()
        {
            InitializeComponent();
            _repository = _mainWindow.AutofacBootStrap.AntecedentsRepository();
            UpdateDataGrid();
        }

        private void AddItem_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var  frm =  new AddEditAnrecedentsView(_repository , new Antecedent());
            frm.UpdateDataDg += UpdateDataGrid;
            frm.Show();
        }

        private void EditCommand_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = gridControl.SelectedItem as Antecedent;
            if (item == null)
            {
                MessageBox.Show("Attention", "Séléctionner un champ !!");
                return;
            }
            var frm = new AddEditAnrecedentsView(_repository, item);
            frm.UpdateDataDg += UpdateDataGrid;
            frm.Show();
        }
        private void DeleteCommand_OnItemClick(object sender, ItemClickEventArgs e)
        {
            
        }

        private void TxtSearch_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
        private void UpdateDataGrid()
        {
          
            gridControl.ItemsSource = _repository.Query().ToList();
        }

    }
}
