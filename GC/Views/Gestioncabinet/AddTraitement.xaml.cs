using System;
using System.Windows;
using DevExpress.Xpf.Bars;
using Gc.Core.Data;
using Gc.Core.DataManage.Core;

namespace GC.Views.Gestioncabinet
{
    /// <summary>
    /// Interaction logic for AddTraitement.xaml
    /// </summary>
    public partial class AddTraitement 
    {
        private readonly IRepository<Traitement> _repository;

        public delegate void UpdateDg();
        public UpdateDg UpdateDataDg;
        readonly Traitement _traitement;
        public AddTraitement(IRepository<Traitement> repository,IRepository<Medicament> repoMedicament , Traitement traitement )
        {
            InitializeComponent();
            _repository = repository;
            DataContext = traitement;
            _traitement = traitement;
            CbMedicament.ItemsSource = repoMedicament.Query();
            
        }

        private void SaveCommand_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataContext as Traitement;
            try
            {
                if(item != null && item.Id == 0)  _repository.Insert(item); else _repository.Update(item);
                _repository.SaveChanges();
                MessageBox.Show("L'enregistrement est terminer avec succés");
                if (UpdateDataDg != null) UpdateDataDg();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);

            }
        }

        private void SaveAndCloseCommand_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataContext as Traitement;
            try
            {
                if (item != null && item.Id == 0) _repository.Insert(item); else _repository.Update(item);
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

        private void SaveAndNewCommand_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataContext as Traitement;
            try
            {
                if (item != null && item.Id == 0) _repository.Insert(item); else _repository.Update(item);_repository.SaveChanges();
                MessageBox.Show("L'enregistrement est terminer avec succés");
                if (UpdateDataDg != null) UpdateDataDg();
                DataContext =  new Traitement {ConsultationId = _traitement.ConsultationId};
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);

            }
        }
    }
}
