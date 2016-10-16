using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gc.Core.Data
{
    public class Consultation : IDataErrorInfo
    {
      public Int32 Id { get; set; }
        public int NumeroConsultation { get; set; }
        public DateTime? DateConsultation { get; set; }
        public decimal Payer  { get; set; }
        public decimal Reste {  get; set; }
        public string Description { get; set; }
        public Int32 DossierPatientId { get; set; }
        public DossierPatient DossierPatient { get; set; }
        public ObservableCollection<Traitement> Traitements { get; set; }
        public ObservableCollection<SyptmomeConsult> SymptomesConsults { get; set; }
        public ObservableCollection<AnalysesDemande> AnalysesDemandes { get; set; }
        public ObservableCollection<CompteRendu> CompteRendus { get; set; }

        public string this[string columnName]
        {
            get
            {
                String errorMessage = String.Empty;
                switch (columnName)
                {
                    case "Payer":
                        if (Payer < 1)
                        {
                            errorMessage = "le payement doit etre supperieur a zéro";
                        }
                        break;
                }
                return errorMessage;
            }
        }
        [NotMapped]
        public string Error { get; private set; }
    }
}
