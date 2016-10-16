using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Gc.Core.Annotations;

namespace Gc.Core.Data
{
    public class ArretTravail:INotifyPropertyChanged
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public int PatientId { get; set; }
        public DateTime? DubeterTravail { get; set; }
        public DossierPatient Patient { get; set; }
        private string _durree;

        public string Durree
        {
            get
            {
                if (_durree == null) return null;
                var val = Regex.Match(_durree, @"\d+").Value;
                try
                {
                    int n = Int32.Parse(val);
                    if (Date != null) DubeterTravail = Date.Value.AddDays(n);
                }
// ReSharper disable once EmptyGeneralCatchClause
                catch (Exception)
                {

                }
                return _durree;
            }
            set
            {
                _durree = value;
              
                OnPropertyChanged(_durree);
            }
        }
        private string _dureeProl;

        public string DureeProlangee
        {
            get
            {
                if (_dureeProl == null) return null;
                var val = Regex.Match(_dureeProl, @"\d+").Value;
                try
                {
                    int n = Int32.Parse(val);
                    if (Date != null)
                    {
                        if (DubeterTravail != null)
                            DubeterTravail =(!string.IsNullOrWhiteSpace(_durree))?DubeterTravail.Value.AddDays(n): Date.Value.AddDays(n);
                        else
                        {
                            DubeterTravail =  Date.Value.AddDays(n);
                        }
                    }
                }
// ReSharper disable once EmptyGeneralCatchClause
                catch (Exception)
                {

                }
                return _dureeProl;
            }
            set
            {
                _dureeProl = value; 
                OnPropertyChanged(_dureeProl);
            }
        }
        
      
      


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
