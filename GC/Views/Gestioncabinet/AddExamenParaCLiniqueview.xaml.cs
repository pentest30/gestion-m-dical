using System;
using System.Linq;
using System.Windows;
using DevExpress.Xpf.Bars;
using Gc.Core.Data;
using Gc.Core.DataManage.Core;


namespace GC.Views.Gestioncabinet
{
    /// <summary>
    /// Interaction logic for AddExamenParaCLiniqueview.xaml
    /// </summary>
    public partial class AddExamenParaCLiniqueview
    {
        private readonly IRepository<AnalysesDemande> _repositoryConsult;
        readonly MainWindow _mainWindow = (MainWindow)Application.Current.MainWindow;
        public delegate void UpdateDg();
        public AddSymptomes.UpdateDg UpdateDataDg;
        public AddExamenParaCLiniqueview(AnalysesDemande analyses)
        {
            InitializeComponent();

            _repositoryConsult = _mainWindow.AutofacBootStrap.RepositoryAnaLysesDemandes(); analyses.ListNames = _repositoryConsult.Query().Select(x => x.Libelle).Distinct().ToList();
            DataContext = analyses;}

        private void AddContinuBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataContext as AnalysesDemande;
            try
            {
                if (item != null && item.Id == 0) _repositoryConsult.Insert(item); else if (item != null) _repositoryConsult.Update(item.Id);
                _repositoryConsult.SaveChanges();
                if (item != null)
                    item.ListNames = _repositoryConsult.Query().Select(x => x.Libelle).Distinct().ToList();
                MessageBox.Show("L'enregistrement est terminer avec succés");
                if (UpdateDataDg != null) UpdateDataDg();
                DataContext = new AnalysesDemande();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);

            }
        }

        private void AddCloseBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataContext as AnalysesDemande;
            try
            {
                if (item != null && item.Id == 0) _repositoryConsult.Insert(item); else if (item != null) _repositoryConsult.Update(item.Id);
                _repositoryConsult.SaveChanges();
                if (item != null)
                    item.ListNames = _repositoryConsult.Query().Select(x => x.Libelle).Distinct().ToList();MessageBox.Show("L'enregistrement est terminer avec succés");
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
    }
}
