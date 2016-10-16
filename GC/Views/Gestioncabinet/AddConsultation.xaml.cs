using System;
using System.Collections.Generic;using System.Windows;
using DevExpress.Xpf.Bars;
using Gc.Core.Data;
using Gc.Core.DataManage.Core;
using GC.MV;
using GC.Views.Reporting;

namespace GC.Views.Gestioncabinet
{
    /// <summary>
    /// Interaction logic for AddConsultation.xaml </summary>
    public partial class AddConsultation {
        readonly MainWindow _mainWindow = (MainWindow)Application.Current.MainWindow;
        readonly IRepository<Consultation> _repositoryConsult;
        readonly IRepository<Traitement> _repositoryMedicament;
        readonly IRepository<Medicament> _repositoryMed;
        readonly IRepository<SyptmomeConsult> _repositorySympConslt;
        public delegate void UpdateDg();
        public UpdateDg UpdateDataDg;
        public AddConsultation(Consultation item)
        {
            InitializeComponent();
            _mainWindow.LoadingDecorator.IsSplashScreenShown = true;
            DataContext = item;
            _repositoryConsult = _mainWindow.AutofacBootStrap.ResoleConsultRepo();
            _repositorySympConslt = _mainWindow.AutofacBootStrap.SymptomeConsultRepo();
            var repositoryDossierPatient = _mainWindow.AutofacBootStrap.ResoleDossierPatientRepo();
            _repositoryMedicament = _mainWindow.AutofacBootStrap.ResoleTraitmentRepo();
            _repositoryMed = _mainWindow.AutofacBootStrap.ResoleMedicamentRepo();
            CbPatients.ItemsSource = repositoryDossierPatient.Query();
            _mainWindow.LoadingDecorator.IsSplashScreenShown = false;
        }

        private void SymAddItem_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var  symp= new SyptmomeConsult();
            var consult = DataContext as Consultation;
           
            if (consult != null && consult.Id > 0)
            {
                symp.ConsultationId = consult.Id;
                AddSympShow(symp);
            }
            else
            {

                var result = MessageBox.Show(" Vous n'avez pas encore sauvegarder la consultation , voulez  vous sauvegarder ?", "Attention", MessageBoxButton.YesNo, MessageBoxImage.Error);
                if (result != MessageBoxResult.Yes) return;
                if (consult == null || consult.DossierPatientId <= 0)
                {
                    MessageBox.Show("Il faut choisir un patient");
                    return;
                }
                if (consult.DateConsultation == null) consult.DateConsultation = DateTime.Now;
                if (consult.Id == 0) _repositoryConsult.Insert(consult);
               // else _repositoryConsult.Update(consult);
                _repositoryConsult.SaveChanges();
                if (UpdateDataDg != null) UpdateDataDg();
                symp.ConsultationId = consult.Id;
                AddSympShow(symp);
            }
            
            
        }

        private void AddSympShow(SyptmomeConsult symp)
        {
            var frm = new AddSymptomes(_repositorySympConslt, symp);
            //frm.UpdateDataDg += UpdateSymDg;
            frm.Show();
        }

        //private void AddGrgBtn_OnItemClick(object sender, ItemClickEventArgs e)
        //{
        //    var  item =  new Traitement();
        //    var consult =  DataContext as Consultation;
        //    if (consult != null&&consult.Id>0)
        //    {
        //        item.ConsultationId = consult.Id;
        //        ShowAddTraitement(item);
        //    }
        //    else
        //    {
        //        var result=MessageBox.Show(" Vous n'avez pas encore sauvegarder la consultation , voulez  vous sauvegarder ?" ,"Attention" , MessageBoxButton.YesNo, MessageBoxImage.Error);
        //        if (result != MessageBoxResult.Yes) return;
        //        if (consult == null || consult.DossierPatientId <= 0)
        //        {
        //            MessageBox.Show("Il faut choisir un patient");
        //            return;
        //        }
        //        if (consult.Id == 0) _repositoryConsult.Insert(consult);
        //        //else _repositoryConsult.Update(consult);
        //        _repositoryConsult.SaveChanges();
        //        if (UpdateDataDg != null) UpdateDataDg();
        //        item.ConsultationId = consult.Id;
        //        ShowAddTraitement(item);
        //    }
        //}

        //private void ShowAddTraitement(Traitement item)
        //{
        //    var frm = new AddTraitement(_repositoryMedicament, _repositoryMed, item);
        //    frm.UpdateDataDg += UpdateGc;
        //    frm.Show();
        //}

       
        private void AddBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataContext as Consultation;
            if (item == null || string.IsNullOrWhiteSpace(item.DateConsultation.ToString()) )
            {
                MessageBox.Show("Valeurs nulles non autorisé", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (item.Id > 0) _repositoryConsult.Update(item);
                else _repositoryConsult.Insert(item);
                _repositoryConsult.SaveChanges();
                MessageBox.Show("L'enregistrement est terminer avec succés");
                if (UpdateDataDg != null) UpdateDataDg();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);

            }
        }

        private void AddAndCloseBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataContext as Consultation;
            if (item == null || string.IsNullOrWhiteSpace(item.DateConsultation.ToString()))
            {
                MessageBox.Show("Valeurs nulles non autorisé", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (item.Id > 0) _repositoryConsult.Update(item);
                else _repositoryConsult.Insert(item);
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

        private void AddAndCtnBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataContext as Consultation;
            if (item == null || string.IsNullOrWhiteSpace(item.DateConsultation.ToString()))
            {
                MessageBox.Show("Valeurs nulles non autorisé", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (item.Id > 0) _repositoryConsult.Update(item);
                else _repositoryConsult.Insert(item);
                _repositoryConsult.SaveChanges();
                MessageBox.Show("L'enregistrement est terminer avec succés");
                if (UpdateDataDg != null) UpdateDataDg();
                DataContext = new Consultation();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);

            }
        }

        private void OrdItem_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataContext as Consultation;
            if (item == null || item.Id == 0)
            {
                MessageBox.Show("Valeurs nulles non autorisé", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var  sources =  new List<OrdenanceVm>();
            foreach (var traitement in item.Traitements)
            {
                var  ord  = new  OrdenanceVm();
                if (item.DateConsultation != null) ord.Date = item.DateConsultation.Value;
                ord.Nom = item.DossierPatient.Nom;
                ord.Prenom = item.DossierPatient.Prenom;
                ord.Age = item.DossierPatient.Age;
                ord.NomMedicale = traitement.Medicament.NomCommercial;
                ord.Qnt = traitement.Qnt.ToString();
                sources.Add(ord);

            }
            var  frm =  new ReportingVIew(sources);
            frm.Show();
        }

        //private void EditCommand_OnItemClick(object sender, ItemClickEventArgs e)
        //{
        //   var  item = GridControlTraitments.SelectedItem as Traitement;
        //    if (item == null || item.Id == 0)
        //    {
        //        MessageBox.Show("Séléctionner un champ", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
        //        return;
        //    }
        //    var frm = new AddTraitement(_repositoryMedicament, _repositoryMed, item);
        //    frm.UpdateDataDg += UpdateGc;
        //    frm.Show();
        //}
        private void CloseBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            Close();
        }
    }
}
