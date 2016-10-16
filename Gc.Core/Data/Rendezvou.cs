using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gc.Core.Annotations;

namespace Gc.Core.Data
{
    public class Rendezvou : IDataErrorInfo, INotifyPropertyChanged
    {
       
        public int Id { get; set; }
        public string Num { get; set; }
        public int PatientId { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }
       // [DataType(DataType.DateTime)]
        public DateTime? HourFrom { get; set; }

        public string Raison { get; set; }

        public DateTime? HourTo { get; set; }
        public string Description { get; set; }
        public bool Urgent { get; set; }
        public DossierPatient Patient { get; set; }

        public string this[string columnName]
        {
            get
            {
                String errorMessage = String.Empty;
                switch (columnName)
                {
                    case "Date":
                        if (String.IsNullOrEmpty(Date.ToString()))
                        {
                            errorMessage = "Le champ Date est requis";
                        }
                        break;
                    //case "HourFrom":
                    //    if (String.IsNullOrEmpty(HourFrom.ToString()))
                    //    {
                    //        errorMessage = "Le champ  est requis";
                    //    }
                    //    break;
                    //case "HourTo":
                    //    if (String.IsNullOrEmpty(HourTo.ToString()))
                    //    {
                    //        errorMessage = "Le champ  est requis";
                    //    }
                    //    break;
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
