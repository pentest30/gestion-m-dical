using System;
using System.Linq;
using System.Windows;
using DevExpress.Xpf.Bars;
using Gc.Core.Data;
using Gc.Core.DataManage.Core;

namespace GC.Views
{
    /// <summary>
    /// Interaction logic for CabinetView.xaml
    /// </summary>
    public partial class CabinetView
    {
        private readonly IRepository<Cabinet> _repository;

        public CabinetView(IRepository<Cabinet> repository )
        {
            _repository = repository;
            InitializeComponent();
            DataContext =( _repository.Query().Count>0)? _repository.Query().FirstOrDefault(): new Cabinet();
        }

        private void AddBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataContext as Cabinet;
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
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);

            }
        }
        public delegate void UpdateDg();
        public UpdateDg UpdateDataDg;

        private void CloseBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
         Close();   
        }
    }
}
