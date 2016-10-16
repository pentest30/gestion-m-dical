using System;
using System.Windows;
using DevExpress.Xpf.Bars;
using Gc.Core.Data;
using Gc.Core.DataManage.Core;


namespace GC.Views.Parametres
{
    /// <summary>
    /// Interaction logic for AddEditExamenClinique.xaml
    /// </summary>
    public partial class AddEditExamenClinique
    {
        readonly IRepository<Symptome> _repository;
        public delegate void UpdateDg();
        public AddEditAnrecedentsView.UpdateDg UpdateDataDg;
        public AddEditExamenClinique(IRepository<Symptome> repository, Symptome symptome)
        {
            InitializeComponent();
            _repository = repository;
            DataContext = symptome;
        }

        private void AddContinuBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataContext as Symptome;
            if (item == null || string.IsNullOrWhiteSpace(item.Libelle))
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
                DataContext = new Symptome();

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);

            }
        }

        private void AddCloseBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataContext as Symptome;
            if (item == null || string.IsNullOrWhiteSpace(item.Libelle))
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

        private void CloseBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            Close();}
    }
}
