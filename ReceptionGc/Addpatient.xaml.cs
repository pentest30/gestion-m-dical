using System;
using System.Windows;
using DevExpress.Xpf.Bars;
using Gc.Core.Data;
using Gc.Core.DataManage.Core;

namespace ReceptionGc
{
    /// <summary>
    /// Interaction logic for Addpatient.xaml
    /// </summary>
    public partial class Addpatient
    {
        private readonly IRepository<DossierPatient> _repository;
        readonly MainWindow _mainWindow = (MainWindow) Application.Current.MainWindow;
        public delegate void UpdateDg();
        public UpdateDg UpdateDataDg;
       
        public Addpatient(IRepository<DossierPatient> repository)
        {

            InitializeComponent();
         //   _mainWindow.LoadingDecorator.IsSplashScreenShown = true;
            DataContext = new DossierPatient();
            _repository = repository;
           
           // _mainWindow.LoadingDecorator.IsSplashScreenShown = false;
        }
        public Addpatient(DossierPatient patient, IRepository<DossierPatient> repository)
        {
            _repository = repository;
            DataContext = patient;
            InitializeComponent();
            
        }

        private void AddBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataContext as DossierPatient;
            if (item == null || string.IsNullOrWhiteSpace(item.Prenom) || string.IsNullOrWhiteSpace(item.Nom))
            {
                MessageBox.Show("Valeurs nulles non autorisé","Attention!" , MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (item.Id > 0) _repository.Update(item);
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
     

        private void AddCloseBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataContext as DossierPatient;
            if (item == null || string.IsNullOrWhiteSpace(item.Prenom) || string.IsNullOrWhiteSpace(item.Nom))
            {
                MessageBox.Show("Valeurs nulles non autorisé", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (item.Id <= 0)
                    _repository.Insert(item);
                else
                    _repository.Update(item);
                _repository.SaveChanges();
                if (UpdateDataDg != null) UpdateDataDg();
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void AddContinuBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataContext as DossierPatient;
            if (item == null || string.IsNullOrWhiteSpace(item.Prenom) || string.IsNullOrWhiteSpace(item.Nom))
            {
                MessageBox.Show("Valeurs nulles non autorisé", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;}
            try
            {
                if (item.Id > 0) _repository.Update(item);
                else _repository.Insert(item);
                _repository.SaveChanges();
                if (UpdateDataDg != null) UpdateDataDg();
                DataContext = new DossierPatient();

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            DataContext = new DossierPatient();
        }

    
        private void CloseBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item =(DossierPatient) DataContext;
            if (item == null || string.IsNullOrWhiteSpace(item.Prenom) || string.IsNullOrWhiteSpace(item.Nom))
            Close();
            else
            {
                MessageBoxResult result = MessageBox.Show("Est vous sur (e)?", "Attention", MessageBoxButton.YesNo,
                    MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes) Close();
            }
        }
    }
}
