using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using DevExpress.Xpf.Bars;
using Gc.Core.Data;
using Gc.Core.DataManage.Core;

namespace GC.Views.Gestioncabinet
{
    /// <summary>
    /// Interaction logic for AddArretTravailView.xaml
    /// </summary>
    public partial class AddArretTravailView
    {
        private readonly IRepository<ArretTravail> _repository;
        readonly MainWindow _mainWindow = (MainWindow)Application.Current.MainWindow;
       
        public AddArretTravailView( IRepository<ArretTravail> repository , ArretTravail arret )
        {
            _repository = repository;
            InitializeComponent();
            var repositoryDossierPatient = _mainWindow.AutofacBootStrap.ResoleDossierPatientRepo();
            CbPatients.ItemsSource = repositoryDossierPatient.Query();
            DataContext = arret;
            if (arret.Id==0)((ArretTravail) DataContext).Date = DateTime.Now;
            //arret = new ArretTravail();
        }

        public AddArretTravailView( IRepository<ArretTravail> repository ,int id)
        {
            _repository = repository;
            InitializeComponent();
            var repositoryDossierPatient = _mainWindow.AutofacBootStrap.ResoleDossierPatientRepo();
            CbPatients.ItemsSource = repositoryDossierPatient.Query();
            DataContext = _repository.FindById(id);
        }
        public delegate void UpdateDg();
        public UpdateDg UpdateDataDg;
        private void AddBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataContext as ArretTravail;
            if (item != null && (item.Date == null  && item.DubeterTravail == null && item.Durree == null &&
                                 item.DureeProlangee == null))
            {
                MessageBox.Show("Valeurs nulles non autorisé", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
           
            try
            {
                if (item != null && item.Id > 0) _repository.Update(item , item.Id);
                else _repository.Insert(item);
                _repository.SaveChanges();
                MessageBox.Show("L'enregistrement est terminer avec succés");
                if (UpdateDataDg != null) UpdateDataDg();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);

            }
        }

        private void AddAndCloseBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataContext as ArretTravail;
            if (item != null && (item.Date == null && item.DubeterTravail == null && item.Durree == null &&
                                item.DureeProlangee == null))
            {
                MessageBox.Show("Valeurs nulles non autorisé", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (item != null && item.Id > 0) _repository.Update(item, item.Id);
                else _repository.Insert(item);
                _repository.SaveChanges();
                MessageBox.Show("L'enregistrement est terminer avec succés");
                if (UpdateDataDg != null) UpdateDataDg();
                DataContext = new ArretTravail();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);

            }
        }

        private void AddAndCtnBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataContext as ArretTravail;
            if (item != null && (item.Date == null && item.DubeterTravail == null && item.Durree == null &&
                                item.DureeProlangee == null))
            {
                MessageBox.Show("Valeurs nulles non autorisé", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }try
            {
                if (item != null && item.Id > 0) _repository.Update(item, item.Id);
                else _repository.Insert(item);
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

        private void BarItem_OnItemClick(object sender, ItemClickEventArgs e)
        {
            Close();
        }

       

        private void Dureetxt_OnSelectionChanged(object sender, RoutedEventArgs e)
        {
           

        }
    }
}
