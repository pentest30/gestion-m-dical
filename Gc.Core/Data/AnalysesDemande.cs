using System;
using System.Collections.Generic;
using System.ComponentModel;
using Gc.Core.Annotations;

namespace Gc.Core.Data
{
    public class AnalysesDemande : INotifyPropertyChanged

    {
        public AnalysesDemande()
        {
            ListeExamen =  new List<string>()
            {
                "Examen biologique",
                "Examen radiologique",
                "Autre"
            };
            
        }
        public int Id { get; set; }
        private string _libelle;

        public string Libelle
        {
            get { return _libelle; }
            set
            {
                _libelle = value;
                OnPropertyChanged(_libelle);
            }
        }

        public string TypeExamen { get; set; }
        public List<string> ListeExamen { get;  set; }

        private List<String> PopulateExamenBio()
        {
            return new List<string>()
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

        public Int32 ConsultationId { get; set; }
        public string Remarques { get; set; }
        //public DateTime? Date { get; set; }

        public Consultation Consultation { get; set; }



        public List<string> ListNames
        {
            get { return PopulateExamenBio(); }
            set { ; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
