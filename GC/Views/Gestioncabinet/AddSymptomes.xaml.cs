using System;
using System.Windows;
using DevExpress.Xpf.Bars;
using Gc.Core.Data;
using Gc.Core.DataManage.Core;


namespace GC.Views.Gestioncabinet
{
    /// <summary>
    /// Interaction logic for AddSymptomes.xaml
    /// </summary>
    public partial class AddSymptomes 
    {
        private readonly IRepository<SyptmomeConsult> _repositoryConsult;
        readonly MainWindow _mainWindow = (MainWindow)Application.Current.MainWindow;
        public delegate void UpdateDg();
        public UpdateDg UpdateDataDg;
        public AddSymptomes(IRepository<SyptmomeConsult> repositoryConsult, SyptmomeConsult symp)
        {
            _repositoryConsult = repositoryConsult;
            var repo = _mainWindow.AutofacBootStrap.SymptomeRepo();
            DataContext = symp;
            InitializeComponent();
            CbSymp.ItemsSource = repo.Query();
        }

   
        private void AddCloseBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            Close();
        }

        private void AddContinuBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataContext as SyptmomeConsult;
            try
            {
                if (item != null && item.Id == 0) _repositoryConsult.Insert(item); else _repositoryConsult.Update(item);
                _repositoryConsult.SaveChanges();
                MessageBox.Show("L'enregistrement est terminer avec succés");
                if (UpdateDataDg != null) UpdateDataDg();
                DataContext = new  SyptmomeConsult();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);

            }
        }

        private void CloseBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataContext as SyptmomeConsult;
            try
            {
                if (item != null && item.Id == 0) _repositoryConsult.Insert(item); else _repositoryConsult.Update(item);
                _repositoryConsult.SaveChanges();
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
