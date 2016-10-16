using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gc.Core.Annotations;

namespace Gc.Core.Data
{
    public class DossierPatient : IDataErrorInfo, INotifyPropertyChanged
    {
        public DossierPatient()
        {

            TitreCiviles = new List<TitreCivile>
            {
                new TitreCivile {Abrv = "M", Titre = "Monsieur"},
                new TitreCivile {Abrv = "Mlle", Titre = "Deuxmoiselle"},
                new TitreCivile {Abrv = "Mme", Titre = "Damme"},

            };
            SexesList =  new List<string>(){"Masculin", "Féminin"};
        }

        public List<String> SexesList { get; set; }
        
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        public string Nis { get; set; }
        public string Genre { get; set; }
        private string _nom;

        public string Nom
        {
            get { return _nom; }
            set
            {
                _nom = value;
                OnPropertyChanged(_nom);
            }
        }

        private string _prenom;

        public string Prenom
        {
            get { return _prenom; }
            set
            {
                _prenom = value;
                OnPropertyChanged(_prenom);
            }
        }


        private DateTime? _date;

        public DateTime? DateNaissance
        {
            get
            {
                
                return _date;
            }
            set
            {
                _date = value; 
                OnPropertyChanged(_date.ToString());
            }
        }
        //public DateTime? DateNaissance { get; set; }
        private int _age;

        public int Age
        {
            get
            {
                
                return  _age;
            }
            set
            {
                if (_date != null) _age = DateTime.Now.Year - _date.Value.Year;
                else
                {
                    _age = value;}
                
               
            }
        }

        public string Sexe { get; set; }
        public decimal Poids { get; set; }
        public decimal Taille { get; set; }

        public string Email { get; set; }
        public string Tel { get; set; }
        public string Addresse { get; set; }
        public ObservableCollection<Consultation> Consultations { get; set; }
        public ObservableCollection<InterventionChirurgicale> InterventionChirurgicaux { get; set; }
        public ObservableCollection<Allergie> Allergies { get; set; }
        public ObservableCollection<Rendezvou> Rendezvous { get; set; }
        public ObservableCollection<ArretTravail> ArretTravails { get; set; }
      
        [NotMapped]
        public List<TitreCivile> TitreCiviles { get; set; }
        public string NomComplet
        {
            get { return Nom + " " + Prenom + " (age: " +Age+ ")"; }
        }
        public string this[string columnName]
        {
            get
            {
                String errorMessage = String.Empty;
                switch (columnName)
                {
                    case "Nom":
                        if (String.IsNullOrEmpty(Nom))
                        {
                            errorMessage = "Le champ Nom est requis";
                        }
                        break;
                    case "Prenom":
                        if (String.IsNullOrEmpty(Prenom))
                        {
                            errorMessage = "Le champ Prénom est requis";
                        }
                        break;
                    case "Age":
                        if (Age < 1)
                        {
                            errorMessage = "L'Age doit etre supperieur a zero";
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
    }}
