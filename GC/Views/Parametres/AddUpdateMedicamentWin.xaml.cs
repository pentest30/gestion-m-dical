using System;
using System.Linq;
using System.Windows;
using DevExpress.Xpf.Bars;
using Gc.Core.Data;
using Gc.Core.DataManage.Core;

namespace GC.Views.Parametres
{
    /// <summary>
    /// Interaction logic for AddUpdateMedicamentWin.xaml
    /// </summary>
    public partial class AddUpdateMedicamentWin 
    {
        private readonly IRepository<Medicament> _repository;
        readonly MainWindow _mainWindow = (MainWindow)Application.Current.MainWindow;
        public AddUpdateMedicamentWin(IRepository<Medicament> repository )
        {
            _repository = repository;
            InitializeComponent();
            var repositoryDci = _mainWindow.AutofacBootStrap.RepositoryDci();
            DataContext =  new Medicament();
            //string[] param = {"Dci"};
            CbDci.ItemsSource =repositoryDci.Query().ToList();
        }

        public AddUpdateMedicamentWin(IRepository<Medicament> repository , Medicament context)
        {
            _repository = repository;
            InitializeComponent();
            var repositoryDci = _mainWindow.AutofacBootStrap.RepositoryDci();
            DataContext =context;
            //string[] param = {"Dci"};
            CbDci.ItemsSource = repositoryDci.Query().ToList();
        }
        public delegate void UpdateDg();
        public UpdateDg UpdateDataDg;
        private void AddBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataContext as Medicament;
            if (item == null)
            {
                MessageBox.Show("Valeurs nulles non autorisé", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (string.IsNullOrWhiteSpace(item.NomCommercial))
                {
                    MessageBox.Show("Valeurs nulles non autorisé", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (item.Id > 0) _repository.Update(item);
                else
                {
                    var dci = CbDci.SelectedItem as Dci;
                    if (dci != null) item.DciId = dci.Id;
                    _repository.Insert(item);
                }
                _repository.SaveChanges();
                MessageBox.Show("L'enregistrement est terminer avec succés");
                if (UpdateDataDg != null) UpdateDataDg();
                //Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);

            }
        }

        private void AddCloseBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataContext as Medicament;
            if (item == null)
            {
                MessageBox.Show("Valeurs nulles non autorisé", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (string.IsNullOrWhiteSpace(item.NomCommercial))
                {
                    MessageBox.Show("Valeurs nulles non autorisé", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (item.Id > 0) _repository.Update(item);
                else
                {
                    var dci = CbDci.SelectedItem as Dci;
                    if (dci != null) item.DciId = dci.Id;
                    _repository.Insert(item);
                }
                _repository.SaveChanges();
                MessageBox.Show("L'enregistrement est terminer avec succés");
                if (UpdateDataDg != null) UpdateDataDg();
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);

            }
        }

        private void CloseBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            Close();
        }

        private void AddContinuBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataContext as Medicament;
            if (item == null)
            {
                MessageBox.Show("Valeurs nulles non autorisé", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (string.IsNullOrWhiteSpace(item.NomCommercial))
                {
                    MessageBox.Show("Valeurs nulles non autorisé", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (item.Id > 0) _repository.Update(item);
                else
                {
                    var dci = CbDci.SelectedItem as Dci;
                    if (dci != null) item.DciId = dci.Id;
                    _repository.Insert(item);
                }
                _repository.SaveChanges();
                MessageBox.Show("L'enregistrement est terminer avec succés");
                if (UpdateDataDg != null) UpdateDataDg();
                DataContext =  new Medicament();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);

            }
        }
    }
}
