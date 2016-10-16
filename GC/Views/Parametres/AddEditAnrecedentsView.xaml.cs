using System;
using System.Windows;
using DevExpress.Xpf.Bars;
using Gc.Core.Data;
using Gc.Core.DataManage.Core;


namespace GC.Views.Parametres
{
    /// <summary>
    /// Interaction logic for AddEditAnrecedentsView.xaml
    /// </summary>
    public partial class AddEditAnrecedentsView
    {
       
        readonly IRepository<Antecedent> _repository;
        public delegate void UpdateDg();
        public UpdateDg UpdateDataDg;
        public AddEditAnrecedentsView(IRepository<Antecedent> repository,Antecedent antecedent  )
        {
            InitializeComponent();
            CbCategorie.ItemsSource = TypeAntecedent.GetAntecedents();
            _repository = repository;
            DataContext  = antecedent;
        }

    
        private void AddCloseBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            
        }

        private void AddContinuBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataContext as Antecedent;
            if (item == null || string.IsNullOrWhiteSpace(item.Libelle) || string.IsNullOrWhiteSpace(item.TypeAntecedent))
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
                DataContext =  new Antecedent();
                
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);

            }
        }

        private void CloseBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataContext as Antecedent;
            if (item == null || string.IsNullOrWhiteSpace(item.Libelle) || string.IsNullOrWhiteSpace(item.TypeAntecedent))
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
    }
}
