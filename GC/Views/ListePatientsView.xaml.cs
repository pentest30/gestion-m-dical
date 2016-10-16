using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DevExpress.Xpf.Bars;
using Gc.Core.Data;
using Gc.Core.DataManage.Core;

using GC.Views.Gestioncabinet;

namespace GC.Views
{
    /// <summary>
    /// Interaction logic for ListePatientsView.xaml
    /// </summary>
    public partial class ListePatientsView
    {
        readonly MainWindow _mainWindow = (MainWindow)Application.Current.MainWindow;
        readonly IRepository<DossierPatient> _repository; 
        int _selecteditem ;
        public ListePatientsView()
        {
            _mainWindow.LoadingDecorator.IsSplashScreenShown = true;
            InitializeComponent();
            _repository = _mainWindow.AutofacBootStrap.ResoleDossierPatientRepo();
            try
            {
                UpdateDataGrid();
            }
            catch (Exception e)
            {
                
                MessageBox.Show(e.Message);
            }
            CbTypeSearch.SelectedIndex = 0;
            _mainWindow.LoadingDecorator.IsSplashScreenShown = false;
            
        }

        private void UpdateDataGrid(){
            _selecteditem  = GridControl.GetRowVisibleIndexByHandle(view.FocusedRowData.RowHandle.Value);

            string[] parm =
            {
                "Consultations", 
                "ArretTravails",
                "InterventionChirurgicaux"};
            GridControl.ItemsSource = _repository.QueryWith(parm).Take(5).ToList();
            GridControl.View.FocusedRowHandle = _selecteditem;
        }


        private void TxtSearch_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            string[] parm =
            {  "Consultations", "Consultations.SymptomesConsults", 
                "ArretTravails",
              "InterventionChirurgicaux"
            };
            //var sq = _repository.QueryWith(parm).ToList();
            var crt = TxtSearch.Text;
            switch (CbTypeSearch.SelectedIndex)
            {
                case 0:

                    try
                    {
                        var q = (from p in _repository.QueryWith(parm)
                            where
                                crt != null &&
                                (p.Nom.Contains(crt) || p.Prenom.Contains(crt) || (p.Tel != null) && p.Tel.Contains(crt))
                            select p).ToList();
                        GridControl.ItemsSource = q;
                    }
                    catch (Exception exception)
                    {

                        MessageBox.Show(exception.Message);
                    }
                    break;
                case 1:

                    try
                    {
                        var q = (from dossierPatient in _repository.QueryWith(parm)
                            from interventionChirurgicale in dossierPatient.InterventionChirurgicaux
                            where interventionChirurgicale.Libelle.ToLower().Contains(crt.ToLower())
                            select dossierPatient).Distinct().ToList();

                        GridControl.ItemsSource = q;
                    }
                    catch (Exception exception)
                    {

                        MessageBox.Show(exception.Message);
                    }
                    break;
                default:
                    try
                    {
                        var q = (from dossierPatient in _repository.QueryWith(parm)
                            from consultation in dossierPatient.Consultations
                            from symptomesConsult in consultation.SymptomesConsults
                            where symptomesConsult.Diagnostic.ToLower().Contains(crt.ToLower())
                            select dossierPatient).Distinct().ToList();

                        GridControl.ItemsSource = q;
                    }
                    catch (Exception exception)
                    {

                        MessageBox.Show(exception.Message);
                    }
                    break;
            }

        }

        private void EditBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var item = GridControl.SelectedItem as DossierPatient;
            if (item == null){
                MessageBox.Show("Attention", "Séléctionner un champ !!");
                return;
            }
            var frm = new AddPatient(item, _repository);
            frm.UpdateDataDg += UpdateDataGrid;
            frm.Show();

        }

       

        private void BarItem_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var frm = new AddPatient(_repository);
            frm.UpdateDataDg += UpdateDataGrid; 
            frm.Show();
        }

        private void ConsultationDataItem_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = GridControl.SelectedItem as DossierPatient;
            var  conslt = new Consultation();
            if (item == null)
            {
                MessageBox.Show("Séléctionner un champ", "Attention", MessageBoxButton.OK , MessageBoxImage.Warning);
                return;
            }
            conslt.DossierPatientId = item.Id;
            conslt.DateConsultation = DateTime.Now;

            //conslt.DossierPatientId
            var frm = new AddConsultation(conslt);
            frm.UpdateDataDg+= UpdateDataGrid;
            frm.Show();
        }

        private void RefreshBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            UpdateDataGrid();
        }

        private void EditCommand_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = GridControl.SelectedItem as DossierPatient;
           
            if (item == null)
            {
                MessageBox.Show("Séléctionner un champ", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            var frm = new AddPatient(item, _repository);
            frm.UpdateDataDg += UpdateDataGrid;  
            frm.Show();
        }

       

        private void DeleteCommand_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var otem = GridControl.SelectedItem as DossierPatient;
            if (otem == null)
            {
                MessageBox.Show("Séléctionner un champ", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var result = MessageBox.Show(" Est vous sur ?", "Attention", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result != MessageBoxResult.Yes) return;
            _repository.Delete(otem.Id);
            _repository.SaveChanges();
            UpdateDataGrid();
        }


        private void GridControl_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = GridControl.SelectedItem as DossierPatient;
            if (item ==null)return;
            var  frm= new ConsultationsView(item);
            frm.UpdateDataDg += UpdateDataGrid;
            frm.Show();
        }

        private void DetailsBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = GridControl.SelectedItem as DossierPatient;
          
            if (item == null)
            {
                MessageBox.Show("Séléctionner un champ", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
           
            var frm = new ConsultationsView(item);
            frm.UpdateDataDg += UpdateDataGrid;
            frm.Show();
        }

        private void SurditeTestBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = GridControl.SelectedItem as DossierPatient;

            if (item == null)
            {
                MessageBox.Show("Séléctionner un champ", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                _mainWindow.ContentControl.Content = null;
                _mainWindow.ContentControl.Content = new GraphView(item);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);//

                //
            }
        }
    }
}
