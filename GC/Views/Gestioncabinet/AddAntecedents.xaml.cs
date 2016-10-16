using System;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Bars;
using Gc.Core.Data;
using Gc.Core.DataManage.Core;


namespace GC.Views.Gestioncabinet
{
    /// <summary>
    /// Interaction logic for AddAntecedents.xaml
    /// </summary>
    public partial class AddAntecedents 
    {
        private readonly InterventionChirurgicale _anntecedent;
        readonly MainWindow _mainWindow = (MainWindow)Application.Current.MainWindow;
        readonly IRepository<InterventionChirurgicale> _repository;
        IRepository<Antecedent> _repoEntecedents;
   
        public AddAntecedents(InterventionChirurgicale anntecedent)
        {
            _anntecedent = anntecedent;
            InitializeComponent();
            DataContext = anntecedent;
             _repoEntecedents = _mainWindow.AutofacBootStrap.AntecedentsRepo();
            _repository = _mainWindow.AutofacBootStrap.AntecedentPatientsRepo();
            CbCategorie.ItemsSource = TypeAntecedent.GetAntecedents();

        }

        private void AddBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataContext as InterventionChirurgicale;
            if (item == null  ){
                MessageBox.Show("Valeurs nulles non autorisé", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
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

        public delegate void UpdateDg();
        public UpdateDg UpdateDataDg;
        private void AddCloseBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataContext as InterventionChirurgicale;
            if (item == null)
            {
                MessageBox.Show("Valeurs nulles non autorisé", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (item.Id > 0) _repository.Update(item);
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

        private void AddContinuBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataContext as InterventionChirurgicale;
          
            if (item == null )
            {
                MessageBox.Show("Valeurs nulles non autorisé", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                int id = item.DossierPatientId;
                if (item.Id > 0) _repository.Update(item);
                else _repository.Insert(item);
                _repository.SaveChanges();
                MessageBox.Show("L'enregistrement est terminer avec succés");
                if (UpdateDataDg != null) UpdateDataDg();
                DataContext =  new InterventionChirurgicale(){DossierPatientId = id};
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

        private void CbCategorie_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
        }
    }
}
