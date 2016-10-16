using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Gc.Core.Annotations;

namespace Gc.Core.Data
{
    public class Medicament : IDataErrorInfo, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Forme { get; set; }
        private string _num;

        public string NumeroEnregistrement
        {
            get { return _num; }
            set
            {
                _num = value;
                OnPropertyChanged(_num);
            }
        }
        
        private string _nomCommercial;
        [Column("NomCommerciale")]
        public string NomCommercial
        {
            get { return _nomCommercial; }
            set
            {
                _nomCommercial = value;
                OnPropertyChanged(_nomCommercial);
            }
        }
        private string _dose;
            
        public string Dose
        {
            get { return _dose; }
            set
            {
                _dose = value;
                OnPropertyChanged(_dose);
            }
        }

       
        public int? DciId { get; set; }
        public string Conditionnement { get; set; }
        public Dci Dci { get; set; }
     

        public string this[string columnName]
        {
            get
            {
                String errorMessage = String.Empty;
                switch (columnName)
                {
                    case "NomCommercial":
                        if (String.IsNullOrEmpty(NomCommercial))
                        {
                            errorMessage = "Le champ Nom est requis";
                        }
                        break;
                 
                    case "Dose":
                        if (String.IsNullOrEmpty(Dose))
                        {
                            errorMessage = "Le champ Dose est requis";
                        }
                        break;
                   
                }
                return errorMessage;
            }
        }
        [NotMapped]
        public string Error { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
