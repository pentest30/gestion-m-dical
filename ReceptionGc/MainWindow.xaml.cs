using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DevExpress.Xpf.Bars;
using Gc.Core;
using Gc.Core.Data;
using Gc.Core.DataManage.Core;

namespace ReceptionGc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly IRepository<DossierPatient> _repository;
        private IRepository<SalleAttente> _repositoryAtt; 
        public AutofacBootStrap AutofacBootStrap { get; set; }


        public MainWindow()
        {
            InitializeComponent();
            AutofacBootStrap =  new AutofacBootStrap();
            _repository = AutofacBootStrap.ResoleDossierPatientRepo();
            _repositoryAtt = AutofacBootStrap.RepositorySalleAtt();
            UpdateDataGrid();
        }

     

        private void BarItem_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var frm = new Addpatient(_repository);
            frm.UpdateDataDg += UpdateDataGrid;
            frm.Show();
        }

        private void UpdateDataGrid()
        {
            GridControl.ItemsSource = _repository.Query();
            string[] param = {"Patient"};
            var d = DateTime.Now.Date.ToString(CultureInfo.InvariantCulture);
            GridControlSalle.ItemsSource =(from p in _repositoryAtt.QueryWith(param).ToList()
                                           where p.DateTime.Date.ToString(CultureInfo.InvariantCulture).Equals(d) 
                                           select p).ToList();
            //throw new System.NotImplementedException();
        }

        private void EditCommand_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = GridControl.SelectedItem as DossierPatient;
            if (item == null)
            {
                MessageBox.Show("Séléctionner un champ !!", "Attention");
                return;
            }
            var frm = new Addpatient(item, _repository);
            frm.UpdateDataDg += UpdateDataGrid;
            frm.Show();
        }

     

        private void RefreshBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            UpdateDataGrid();
        }

        private void GridControl_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void TxtSearch_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var crt = TxtSearch.Text;
            var q = (from p in _repository.Query()
                     where
                         crt != null &&
                         (p.Nom.Contains(crt) || p.Prenom.Contains(crt) || (p.Tel != null) && p.Tel.Contains(crt))
                     select p).ToList();
            GridControl.ItemsSource = q;
        }

        private void DeleteAttCommand_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var otem = GridControlSalle.SelectedItem as SalleAttente;
            if (otem == null)
            {
                MessageBox.Show("Séléctionner un champ", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var result = MessageBox.Show(" Est vous sur ?", "Attention", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result != MessageBoxResult.Yes) return;
            _repositoryAtt.Delete(otem.Id);
            _repositoryAtt.SaveChanges();
            UpdateDataGrid();
        }

        private void RefreshAttBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            string[] param = { "Patient" };
            var d = DateTime.Now.Date.ToString(CultureInfo.InvariantCulture);
            GridControlSalle.ItemsSource = (from p in _repositoryAtt.QueryWith(param).ToList()
                                            where p.DateTime.Date.ToString(CultureInfo.InvariantCulture).Equals(d)
                                            select p).ToList();
        }

        private void AddAtt_OnItemClick(object sender, ItemClickEventArgs e)
        {
             var otem = GridControl.SelectedItem as DossierPatient;
            if (otem == null)
            {
                MessageBox.Show("Séléctionner un champ", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var att = new SalleAttente{PatientId = otem.Id, DateTime =  DateTime.Now, Numero =((List<SalleAttente>) GridControlSalle.ItemsSource).Count+ 1};
            _repositoryAtt.Insert(att);
            _repositoryAtt.SaveChanges();
            UpdateDataGrid();
        }
    }
}
