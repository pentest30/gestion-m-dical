using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using DevExpress.Xpf.Bars;
using Gc.Core.Data;
using Gc.Core.DataManage.Core;


namespace GC.Views.Gestioncabinet
{
    /// <summary>
    /// Interaction logic for CompteRenduView.xaml
    /// </summary>
    public partial class CompteRenduView
    {
        readonly MainWindow _mainWindow = (MainWindow)Application.Current.MainWindow;
        readonly IRepository<CompteRendu> _repositoryCompteRndu;
       
        public delegate void UpdateDg();public UpdateDg UpdateDataDg;
        public CompteRenduView(CompteRendu compte , IRepository<CompteRendu>repository )
        {
            _mainWindow.LoadingDecorator.IsSplashScreenShown = true;
            InitializeComponent();
            _repositoryCompteRndu = repository;
            DataContext = compte;
            UpdateAutoComplete();_mainWindow.LoadingDecorator.IsSplashScreenShown = false;
        }

        private void AddBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataContext as CompteRendu;
            if (item == null || string.IsNullOrWhiteSpace(item.Organe) || string.IsNullOrWhiteSpace(item.Observation))
            {
                MessageBox.Show("Valeurs nulles non autorisé", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (item.Id > 0) _repositoryCompteRndu.Update(item.Id);
                else
                {
                    _repositoryCompteRndu.Insert(item);
                    
                }
                _repositoryCompteRndu.SaveChanges();
                item.Organes = new ObservableCollection<string>(_repositoryCompteRndu.Query().Select(x => x.Organe).Distinct());
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
            var item = DataContext as CompteRendu;
            if (item == null || string.IsNullOrWhiteSpace(item.Organe) || string.IsNullOrWhiteSpace(item.Observation))
            {
                MessageBox.Show("Valeurs nulles non autorisé", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (item.Id > 0) _repositoryCompteRndu.Update(item.Id);
                else
                {
                    _repositoryCompteRndu.Insert(item);
                }
                _repositoryCompteRndu.SaveChanges();
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
            var item = DataContext as CompteRendu;
            if (item == null || string.IsNullOrWhiteSpace(item.Organe) || string.IsNullOrWhiteSpace(item.Observation))
            {
                MessageBox.Show("Valeurs nulles non autorisé", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (item.Id > 0) _repositoryCompteRndu.Update(item.Id);
                else
                {
                    _repositoryCompteRndu.Insert(item);
                   
                }
                item.Organes = new ObservableCollection<string>(_repositoryCompteRndu.Query().Select(x => x.Organe).Distinct());
                _repositoryCompteRndu.SaveChanges();
                MessageBox.Show("L'enregistrement est terminer avec succés");
                if (UpdateDataDg != null) UpdateDataDg();
                DataContext  = new CompteRendu();
                //UpdateAutoComplete();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);

            }
        }

        void UpdateAutoComplete()
        {
            var compte = DataContext as CompteRendu;
            if (compte != null)
                compte.Organes = new ObservableCollection<string>(_repositoryCompteRndu.Query().Select(x => x.Organe).Distinct());
        }

        private void CloseBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            Close();
        }
    }
}
