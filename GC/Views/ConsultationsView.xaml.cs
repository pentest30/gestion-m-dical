using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using Gc.Core.Data;
using Gc.Core.DataManage.Core;
using GC.MV;
using GC.Views.Gestioncabinet;
using GC.Views.Parametres;
using GC.Views.Reporting;

namespace GC.Views
{
    /// <summary>
    /// Interaction logic for ConsultationsView.xaml
    /// </summary>
    public partial class ConsultationsView
    {
        readonly MainWindow _mainWindow = (MainWindow)Application.Current.MainWindow;
         IRepository<Consultation> _repositoryConsult;
        public IRepository<Traitement> RepositoryMedicament { get; private set; }
         IRepository<DossierPatient> _repositoryPatient;
        private readonly IRepository<SyptmomeConsult> _repositorySympConslt;
        private readonly IRepository<Traitement> _repositoryMedicament;
        private readonly IRepository<Medicament> _repositoryMed;
        readonly IRepository<InterventionChirurgicale> _repositoryAntecedent;
        readonly IRepository<ArretTravail> _arretTravailRepository;
        readonly IRepository<CompteRendu> _repositoryCompteRndu;
        public delegate void UpdateDg();
        public UpdateDg UpdateDataDg;
        int _seletedindex;
        private readonly IRepository<AnalysesDemande> _repositoryParaClinique;
        private int _typeExamenId=-1;
        public ConsultationsView(DossierPatient patient)
        {
            _mainWindow.LoadingDecorator.IsSplashScreenShown = true;
            InitializeComponent();
            DataContext = patient;
            _repositorySympConslt = _mainWindow.AutofacBootStrap.SymptomeConsultRepo();
            _repositoryMedicament = _mainWindow.AutofacBootStrap.ResoleTraitmentRepo();
            _repositoryMed = _mainWindow.AutofacBootStrap.ResoleMedicamentRepo();
            _repositoryPatient = _mainWindow.AutofacBootStrap.ResoleDossierPatientRepo();
            _repositoryParaClinique = _mainWindow.AutofacBootStrap.RepositoryAnaLysesDemandes();
            _repositoryAntecedent = _mainWindow.AutofacBootStrap.AntecedentPatientsRepo();
            _arretTravailRepository = _mainWindow.AutofacBootStrap.ArretTravailRepo();
            _repositoryCompteRndu = _mainWindow.AutofacBootStrap.RepositoryCompteRendu();
            // this.TabItem.Content = null;
            //this.TabItem.Content = new GraphView(patient);
            ExamenCliniqueGrid.IsEnabled = false;
            ExamenParaCliniqueGrid.IsEnabled = false;
            TraitemetsGrid.IsEnabled = false;
            ComteRenduGrid.IsEnabled = false;
            _mainWindow.LoadingDecorator.IsSplashScreenShown = false;

        }

        private void AddItem_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataContext as DossierPatient;
            var conslt = new Consultation();
            if (item != null) conslt.DossierPatientId = item.Id;
            conslt.DateConsultation = DateTime.Now;
            //conslt.DossierPatientId
            var frm = new AddConsultation(conslt);
             frm.UpdateDataDg += UpdateDataCOntext;

            frm.Show();
        }

        private void UpdateDataCOntext()
        {
            _seletedindex = DataGrid.GetRowVisibleIndexByHandle(view.FocusedRowData.RowHandle.Value);
            var item = DataContext as DossierPatient;
            _repositoryPatient = null;
            _repositoryPatient = _mainWindow.AutofacBootStrap.ResoleDossierPatientRepo();
            string[] parm =
            {
                "Consultations", 
                "ArretTravails",
                "InterventionChirurgicaux"
            };
            DataContext = _repositoryPatient.QueryWith(x => x.Id == item.Id, parm).FirstOrDefault();
            DataGrid.View.FocusedRowHandle = _seletedindex;
            if (UpdateDataDg != null) UpdateDataDg();
        }

        private void DataGrid_OnSelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
        {
            var selecetdItem = DataGrid.SelectedItem as Consultation;
            if (selecetdItem==null)return;
            var  list1 =_repositorySympConslt.FindBy(w => w.ConsultationId == selecetdItem.Id).ToList();
            var list2 =
                _repositoryParaClinique.FindBy(w => w.ConsultationId == selecetdItem.Id).ToList();
            GridControlSymptome.ItemsSource = list1;
            GridControlAnalyse.ItemsSource =list2;
            AutoCompleteBoxBilan.ItemsSource = PopulateExamenBio();
            string[] parm =
            {
                
                "Medicament"
            };
            var list3 =   _repositoryMedicament.QueryWith(w => w.ConsultationId == selecetdItem.Id, parm).ToList();
            GridControlTraitments.ItemsSource = list3;
            var list4 = _repositoryCompteRndu.FindBy(x => x.ConsultationId == selecetdItem.Id).ToList();
            GridControlConteRendu.ItemsSource = list4;
            GridControlTraitments.View.FocusedRowHandle = -1;
            if (list1.Count == 0) ExamenCliniqueGrid.IsEnabled = false;
            if (list2.Count == 0) ExamenParaCliniqueGrid.IsEnabled = false;
            if (list3.Count == 0) TraitemetsGrid.IsEnabled = false;
            if (list4.Count == 0) ComteRenduGrid.IsEnabled = false;
            ExamenCliniqueAutoCompleteBox.ItemsSource = _repositorySympConslt.Query().Select(x => x.Examen).Distinct().ToList();
           
            PopulateCbMedicament();
            PopulateCompteRendu();
        }

        private void PopulateCbMedicament()
        {
          
            CbMedicament.ItemsSource = _repositoryMed.Query();
        }

        private List<String> PopulateExamenBio()
        {
            return new List<string>
            {
                "FNS + équilibre leucocytaire","groupage sanguin",
                "glycémie à jeun ",
                "hémoglobine glyquée(HbA1c)",
                "bilan d'hémostase TP TCA TCK TQ INR TS",
                "bilan rénal :urée créatinine",
                "bilan inflammatoire :VS CRP",
                "bilan thyroidien T3 T4 TSH",
                "ASLO",
                "IDR a la tuberculine"

            };

        } 
        private void EditCommand_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataGrid.SelectedItem as Consultation;
            
            if (item == null)
            {
                MessageBox.Show("Séléctionner un champ!!", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            //conslt.DossierPatientId
            var frm = new AddConsultation(item);
            frm.Show();
        }

        private void DeleteCommand_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataGrid.SelectedItem as Consultation;
            if (item == null)
            {
                MessageBox.Show("Séléctionner un champ!!", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var result = MessageBox.Show("Est vous sur(e)", "Attention", MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;

            try
            {
                var compteRendus = GridControlConteRendu.ItemsSource as List<CompteRendu>;
                var analysesDemandes = GridControlAnalyse.ItemsSource as List<AnalysesDemande>;
                var syptmomeConsults = GridControlSymptome.ItemsSource as List<SyptmomeConsult>;
                var traitements = GridControlTraitments.ItemsSource as List<Traitement>;
                if (traitements != null && traitements.Count > 0)
                {
                    foreach (var traitement in traitements)
                    {
                        _repositoryMedicament.Delete(traitement);
                    }
                    _repositoryMedicament.SaveChanges();
                }
                if (syptmomeConsults != null && syptmomeConsults.Count > 0)
                {
                    foreach (var syptmomeConsult in syptmomeConsults)
                    {
                        _repositoryConsult.Delete(syptmomeConsult);
                    }
                    _repositoryConsult.SaveChanges();
                }
                if (analysesDemandes != null && analysesDemandes.Count > 0)
                {

                    foreach (var analysesDemande in analysesDemandes)
                    {
                        _repositoryParaClinique.Delete(analysesDemande);
                    }
                    _repositoryParaClinique.SaveChanges();
                }
                if (compteRendus != null &&
                    (compteRendus.Count > 0))
                {
                    foreach (var compteRendu in compteRendus)
                    {
                        _repositoryCompteRndu.Delete(compteRendu);
                    }
                    _repositoryCompteRndu.SaveChanges();
                }

                _repositoryConsult.Delete(item);
                _repositoryConsult.SaveChanges();
                ConsultationsView_OnLoaded(null, new RoutedEventArgs());
            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message);
            }

        }

        private void ConsultationsView_OnLoaded(object sender, RoutedEventArgs e)
        {
           
            _mainWindow.LoadingDecorator.IsSplashScreenShown = true;
            _repositoryPatient = _mainWindow.AutofacBootStrap.ResoleDossierPatientRepo();
            RepositoryMedicament = _mainWindow.AutofacBootStrap.ResoleTraitmentRepo();
            //UpdatamainDg(_repositoryPatient);
        
            _repositoryConsult = _mainWindow.AutofacBootStrap.ResoleConsultRepo();
            _mainWindow.LoadingDecorator.IsSplashScreenShown = false;
        }
  
        private void edit_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            var dateTime = ((DateEdit) sender).DateTime;
            var selectedDate = dateTime.Date.ToString(CultureInfo.InvariantCulture);
            var nextday = dateTime.AddDays(1).Date.ToString(CultureInfo.CurrentUICulture);
            DataGrid.FilterString = string.Format("[DateConsultation] >=#{0}# and [DateConsultation]<#{1}#",
                selectedDate, nextday);
        }

        private void AddAntcedItem_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataContext as DossierPatient;
            var antecedent = new InterventionChirurgicale();
            if (item != null) antecedent.DossierPatientId = item.Id;

            var frm = new AddAntecedents(antecedent);
            frm.UpdateDataDg += UpdateDataCOntext;frm.Show();
            
        }

       
        private void AddGrgBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var consult = DataGrid.SelectedItem as Consultation;
            if (consult == null)
            {
                MessageBox.Show("Séléctionner un champ consultation", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var list = (List<Traitement>)GridControlTraitments.ItemsSource;
            if (list == null) return;
            list.Add(new Traitement());
            GridControlTraitments.View.FocusedRowHandle = list.Count - 1;
            var context = list.Last();
            context.ConsultationId = consult.Id;

            TraitemetsGrid.DataContext = context;
            TraitemetsGrid.IsEnabled = true;
        }

        private void OrdBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataGrid.SelectedItem as Consultation;
            if (item == null || item.Id == 0)
            {
                MessageBox.Show("Valeurs nulles non autorisé", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var sources = new List<OrdenanceVm>();
            var repo = _mainWindow.AutofacBootStrap.RepositoryCabinet();
            var cab = repo.Query().FirstOrDefault();
            var traitements = GridControlTraitments.ItemsSource as List<Traitement>;
            if (traitements != null)
                foreach (var traitement in traitements)
                {
                    if (traitement.Medicament== null)continue;
                    var ord = new OrdenanceVm();
                    ord.Date = item.DateConsultation ?? DateTime.Now;
                    ord.Nom = item.DossierPatient.Nom;
                    ord.Prenom = item.DossierPatient.Prenom;
                    ord.Age = item.DossierPatient.Age;
                    ord.NomMedicale = traitement.Medicament.NomCommercial + " " +traitement.Medicament.Dose + Environment.NewLine + " "+ traitement.Description;
                    ord.Qnt = traitement.Qnt + " / " + traitement.NbrJours + " Jrs";
                    if (cab != null)
                    {
                        ord.NomCabinet = cab.NomCabinet;
                        ord.NomMedecin = cab.NomMedecin;
                        ord.PrenomMedecin = cab.PrenomMedecin;
                        ord.NomAr = cab.NomAr;
                        ord.PrenomAr = cab.PrenomAr;
                        ord.Tel = cab.Tel;
                        ord.Addresse = cab.Addresse;
                        ord.Specialite = cab.Specialite;
                        ord.SpecialiteAr = cab.SpecialiteAr;

                    }
                    sources.Add(ord);

                }
            var frm = new ReportingVIew(sources);
            frm.Show();
        }

        private void SymtEditItem_OnItemClick(object sender, ItemClickEventArgs e)
        {

            var item = GridControlSymptome.SelectedItem as SyptmomeConsult;
           // ExamenCliniqueGrid.DataContext = null;
            //GridControlSymptome.SelectedItem = null;
            try
            {

                if (item != null && item.Id == 0)
                {
                    if (string.IsNullOrWhiteSpace(item.Examen))
                    {
                        MessageBox.Show("Spécifier le type d'examen", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    var consultation = DataGrid.SelectedItem as Consultation;
                    if (consultation != null)
                        item.ConsultationId = consultation.Id;
                    _repositorySympConslt.Insert(item);
                    _repositorySympConslt.SaveChanges();
                    MessageBox.Show("L'enregistrement est terminer avec succés");
                    UpdateDataCOntext();
                    SymtNewItem_OnItemClick(sender,e);


                }
                else
                {
                    _repositorySympConslt.Update(item);
                    _repositorySympConslt.SaveChanges();
                    MessageBox.Show("L'enregistrement est terminer avec succés");
                    UpdateDataCOntext();
                }
              
                
              
                //DataContext = new SyptmomeConsult();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);

            }
        }

        private void DeleteSympItem_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = GridControlSymptome.SelectedItem as SyptmomeConsult;
            if (item == null)
            {
                MessageBox.Show("Séléctionner un champ!!", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var result = MessageBox.Show("Est vous sur(e)", "Attention", MessageBoxButton.YesNo,
             MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;
            _repositorySympConslt.Delete(item.Id);
            _repositorySympConslt.SaveChanges();
            UpdateDataCOntext();
        }

      
      

        private void NewCommandParaclinique_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var consult = DataGrid.SelectedItem as Consultation;
            if (consult == null)
            {
                MessageBox.Show("Séléctionner un champ consultation", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var list = (List<AnalysesDemande>)GridControlAnalyse.ItemsSource;
            if (list == null) return;
            list.Add(new AnalysesDemande() );
           
            GridControlAnalyse.View.FocusedRowHandle = list.Count - 1;
            var context = list.Last();
            context.ConsultationId = consult.Id;
           
            ExamenParaCliniqueGrid.DataContext =context;
            if (_typeExamenId > -1) CbTypeAnalyse.SelectedIndex = _typeExamenId;
            ExamenParaCliniqueGrid.IsEnabled = true;
        }

      
    

        private void BilanBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataGrid.SelectedItem as Consultation;
            if (item == null || item.Id == 0)
            {
                MessageBox.Show("Valeurs nulles non autorisé", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var sources = new List<OrdenanceVm>();
            var repo = _mainWindow.AutofacBootStrap.RepositoryCabinet();
            var cab = repo.Query().FirstOrDefault();
            var lst = ((List<AnalysesDemande>)GridControlAnalyse.ItemsSource).Where(x =>x.TypeExamen!=null&& x.TypeExamen.Equals("Examen biologique"));
            foreach (var traitement in lst)
            {
                var ord = new OrdenanceVm();
                ord.Date = item.DateConsultation ?? DateTime.Now;
                ord.Nom = item.DossierPatient.Nom;
                ord.Prenom = item.DossierPatient.Prenom;
                ord.Age = item.DossierPatient.Age;
                ord.BilanName = traitement.Libelle;
                if (cab != null)
                {
                    ord.NomCabinet = cab.NomCabinet;
                    ord.NomMedecin = cab.NomMedecin;
                    ord.PrenomMedecin = cab.PrenomMedecin;
                    ord.NomAr = cab.NomAr;
                    ord.PrenomAr = cab.PrenomAr;
                    ord.Tel = cab.Tel;
                    ord.Addresse = cab.Addresse;
                    ord.Specialite = cab.Specialite;
                    ord.SpecialiteAr = cab.SpecialiteAr;

                }
               // ord.Qnt = traitement.Qnt + " / " + traitement.NbrJours + " Jrs";
                sources.Add(ord);

            }
            var frm = new BilanReportingView(sources);
            frm.Show();
        }

        private void EditCommandAntecedent_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = GridControlAntecedent.SelectedItem as InterventionChirurgicale;
            if (item == null)
            {
                MessageBox.Show("Séléctionner un champ", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var frm = new AddAntecedents(item);
            frm.UpdateDataDg += UpdateDataCOntext; frm.Show();
        }

        private void DeleteCommandAntecedent_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = GridControlAntecedent.SelectedItem as InterventionChirurgicale;
            if (item == null)
            {
                MessageBox.Show("Séléctionner un champ", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            var result = MessageBox.Show("Est vous sur(e)", "Attention", MessageBoxButton.YesNo,
           MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;
            _repositoryAntecedent.Delete(item.Id);
            _repositoryAntecedent.SaveChanges();
            UpdateDataCOntext();
        }

        private void AddArretItem_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataContext as DossierPatient;
            if (item == null) return;
            var frm = new AddArretTravailView(_arretTravailRepository, new ArretTravail {PatientId = item.Id});
            frm.UpdateDataDg += UpdateDataCOntext;
            frm.Show();
        }

        private void EditArretCommand_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataGridArret.SelectedItem as ArretTravail;
            if (item == null)
            {
                MessageBox.Show("Séléctionner un champ", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var frm = new AddArretTravailView(_arretTravailRepository,item.Id);
            frm.UpdateDataDg += UpdateDataCOntext;
            frm.Show();
        }

        private void PrintArretCommand_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataGridArret.SelectedItem as ArretTravail;
            if (item == null || item.Id == 0)
            {
                MessageBox.Show("Valeurs nulles non autorisé", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var sources = new List<OrdenanceVm>();
            var ord = new OrdenanceVm();
            var repo = _mainWindow.AutofacBootStrap.RepositoryCabinet();
            var cab = repo.Query().FirstOrDefault();

            ord.Nom = item.Patient.Nom;
            ord.Prenom = item.Patient.Prenom;
            ord.Duree = item.Durree;
            ord.DureeProlange = item.DureeProlangee;
            ord.DatePrint = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(ord.Duree)) ord.Date = item.Date;
            if (!string.IsNullOrWhiteSpace(item.DureeProlangee)) ord.DateProlangation = item.Date;
            ord.DubetTravail = item.DubeterTravail;
            ord.Age = item.Patient.Age;
            if (cab != null)
            {
                ord.NomCabinet = cab.NomCabinet;
                ord.NomMedecin = cab.NomMedecin;
                ord.PrenomMedecin = cab.PrenomMedecin;
                ord.NomAr = cab.NomAr;
                ord.PrenomAr = cab.PrenomAr;
                ord.Tel = cab.Tel;
                ord.Addresse = cab.Addresse;
                ord.Specialite = cab.Specialite;
                ord.SpecialiteAr = cab.SpecialiteAr;

            }
            sources.Add(ord);
            var frm = new ReportingArretview(sources);
            frm.Show();
        }

     

      

        private void AddCrBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var consult = DataGrid.SelectedItem as Consultation;
            if (consult == null)
            {
                MessageBox.Show("Séléctionner un champ consultation", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var list = (List<CompteRendu>)GridControlConteRendu.ItemsSource;
            if (list == null) return;
            list.Add(new CompteRendu {ConsultationId = consult.Id});
            GridControlConteRendu.View.FocusedRowHandle = list.Count - 1;
            ComteRenduGrid.DataContext = list.Last();
            ComteRenduGrid.IsEnabled = true;
        }

    

        private void PrintCrCommand_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataGrid.SelectedItem as Consultation;
            if (item == null || item.Id == 0)
            {
                MessageBox.Show("Valeurs nulles non autorisé", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var sources = new List<CompteRebduVm>();

            List<CompteRendu> compteRendus = GridControlConteRendu.ItemsSource as List<CompteRendu>;
            if (compteRendus != null)
                foreach (var traitement in compteRendus) 
                {

                    var ord = new CompteRebduVm();
                    ord.Date = item.DateConsultation ?? DateTime.Now;
                    ord.Nom = item.DossierPatient.Nom;
                    ord.Prenom = item.DossierPatient.Prenom;
                    ord.Age = item.DossierPatient.Age;

                    ord.Organe = traitement.Organe;

                    ord.Observation = traitement.Observation;
                    sources.Add(ord);

                }
            var frm = new ReportingCompteRenduView(sources);
            frm.Show();
        }

        private void DeleteArretCommand_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = DataGridArret.SelectedItem as ArretTravail;
            if (item == null)
            {
                MessageBox.Show("Séléctionner un champ", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var result = MessageBox.Show("Est vous sur(e)", "Attention", MessageBoxButton.YesNo,
           MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;
            _arretTravailRepository.Delete(item.Id);
            _arretTravailRepository.SaveChanges();
            UpdateDataCOntext();
        }

        private void DeleteCrCommand_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = GridControlConteRendu.SelectedItem as CompteRendu;
            if (item == null)
            {
                MessageBox.Show("Séléctionner un champ", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var result = MessageBox.Show("Est vous sur(e)", "Attention", MessageBoxButton.YesNo,
           MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;
            _repositoryCompteRndu.Delete(item.Id);
            _repositoryCompteRndu.SaveChanges();
            UpdateDataCOntext();
        }

        private void SymtNewItem_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var list = (List<SyptmomeConsult>)GridControlSymptome.ItemsSource;
            if (list == null) return;
            list.Add(new SyptmomeConsult());
            GridControlSymptome.View.FocusedRowHandle = list.Count-1;
            ExamenCliniqueGrid.IsEnabled = true;
        }

        private void CancelSympItem_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var list = (List<SyptmomeConsult>)GridControlSymptome.ItemsSource;
            if (list == null) return;
            var item = list.Last();
            if (item == null) return;
            if (item.Id == 0)
                try
                {
                    list.Remove(item);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            
            UpdateDataCOntext();
        }

        private void GridControlSymptome_OnSelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
        {
            ExamenCliniqueGrid.DataContext = GridControlSymptome.SelectedItem as SyptmomeConsult;
            ExamenCliniqueGrid.IsEnabled = true;
        }

        private void AnalyseEditItem_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = ExamenParaCliniqueGrid.DataContext as AnalysesDemande;
            if (item != null && item.Id == 0)
            {
                if (string.IsNullOrWhiteSpace(item.TypeExamen))
                {
                    MessageBox.Show("Le champ Type d'examen est obligatoire", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                _repositoryParaClinique.Insert(item);
                _repositoryParaClinique.SaveChanges();

                MessageBox.Show("L'enregistrement est terminer avec succés");
                UpdateDataCOntext();
                NewCommandParaclinique_OnItemClick(sender,e);
            } 
            else if (item != null)
            {
                _repositoryParaClinique.Update(item );
                _repositoryParaClinique.SaveChanges();

                MessageBox.Show("L'enregistrement est terminer avec succés");
                UpdateDataCOntext();
            }
            
        }

      

        private void GridControlAnalyse_OnSelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
        {
            ExamenParaCliniqueGrid.DataContext = GridControlAnalyse.SelectedItem as AnalysesDemande;
            ExamenParaCliniqueGrid.IsEnabled = true;
        }

        private void DeleteAnalyseItem_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = GridControlAnalyse.SelectedItem as AnalysesDemande;
            if (item == null)
            {
                MessageBox.Show("Séléctionner un champ", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var result = MessageBox.Show("Est vous sur(e)", "Attention", MessageBoxButton.YesNo,
           MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;
            _repositoryParaClinique.Delete(item.Id);
            _repositoryParaClinique.SaveChanges();
            UpdateDataCOntext();
        }

        private void CancelAnalyseItem_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var list = (List<AnalysesDemande>)GridControlAnalyse.ItemsSource;
            if (list == null) return;
            var item = list.Last();
            if (item == null) return;
            if (item.Id == 0)
                try
                {
                    list.Remove(item);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }

            UpdateDataCOntext();
        }

        private void CbTypeAnalyse_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _typeExamenId = CbTypeAnalyse.SelectedIndex;
        }

        private void CancelTreatCommand_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var list = (List<Traitement>)GridControlTraitments.ItemsSource;
            if (list == null) return;
            var item = list.Last();
            if (item==null) return;
            if (item.Id == 0)
                try
                {
                    list.Remove(item);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }

            UpdateDataCOntext();
        }

        private void ValideTreatBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item =  TraitemetsGrid.DataContext as Traitement;
           var medicament = CbMedicament.SelectedItem as Medicament;
         
            try
            {
                if (item != null && item.Id == 0)
                {
                    if (medicament != null) item.Medicament = medicament;
                    _repositoryMedicament.Insert(item);
                    _repositoryMedicament.SaveChanges();
                    MessageBox.Show("L'enregistrement est terminer avec succés");
                    UpdateDataCOntext();
                    AddGrgBtn_OnItemClick(sender, e);

                }
                else
                {
                    if (item != null) _repositoryMedicament.Update(item, item.Id);
                    _repositoryMedicament.SaveChanges();
                    MessageBox.Show("L'enregistrement est terminer avec succés");
                    UpdateDataCOntext();
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);

            }
        }

        private void GridControlTraitments_OnSelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
        {
            TraitemetsGrid.DataContext = GridControlTraitments.SelectedItem as Traitement;
            //var traitement = GridControlTraitments.SelectedItem as Traitement;
            //if (traitement != null)
            //    CbMedicament.SelectedItem = traitement.Medicament;
            TraitemetsGrid.IsEnabled = true;
        }

        private void DeleteTreatCommand_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = GridControlTraitments.SelectedItem as Traitement;
            if (item == null)
            {
                MessageBox.Show("Séléctionner un champ", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var result = MessageBox.Show("Est vous sur(e)", "Attention", MessageBoxButton.YesNo,
           MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;
            _repositoryMedicament.Delete(item.Id);
            _repositoryMedicament.SaveChanges();
            UpdateDataCOntext();
        }

        private void NewMedicamntBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var  frm =  new AddUpdateMedicamentWin(_repositoryMed);
            frm.UpdateDataDg += PopulateCbMedicament;
            frm.Show();
        }

        void PopulateCompteRendu()
        {
            AutoCompleteBoxCompteRendu.ItemsSource =
                _repositoryCompteRndu.Query().Select(x => x.Organe).Distinct().ToList();
        }
        private void ValideCrBtn_OnItemClick(object sender, ItemClickEventArgs e)
        {
           
            var item = ComteRenduGrid.DataContext as CompteRendu;
            if (item == null || string.IsNullOrWhiteSpace(item.Organe) || string.IsNullOrWhiteSpace(item.Observation))
            {
                MessageBox.Show("Valeurs nulles non autorisé", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (item.Id > 0)
                {
                    _repositoryCompteRndu.Update(item);
                    _repositoryCompteRndu.SaveChanges();
                    MessageBox.Show("L'enregistrement est terminer avec succés");
                    UpdateDataCOntext();
                }
                else
                {
                    _repositoryCompteRndu.Insert(item);
                    _repositoryCompteRndu.SaveChanges();
                    MessageBox.Show("L'enregistrement est terminer avec succés");
                    UpdateDataCOntext();
                    PopulateCompteRendu();
                    AddCrBtn_OnItemClick(sender,e);

                }
             

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);

            }
        }

        private void CancelCrCommand_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var list = (List<CompteRendu>)GridControlConteRendu.ItemsSource;
            if (list == null) return;
            var item = list.Last();
            if (item == null) return;
            if (item.Id == 0)
                try
                {
                    list.Remove(item);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }

            UpdateDataCOntext();
        }

        private void GridControlConteRendu_OnSelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
        {
            ComteRenduGrid.DataContext = GridControlConteRendu.SelectedItem as CompteRendu;
            ComteRenduGrid.IsEnabled = true;
        }
    }
}
