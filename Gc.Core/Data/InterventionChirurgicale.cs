using System;
using System.ComponentModel.DataAnnotations;

namespace Gc.Core.Data
{
    public class InterventionChirurgicale
    {
        [Key]
        public Int32 Id { get; set; }
        public int DossierPatientId { get; set; }
        public string Libelle { get; set; }
        public string TypeAntecedent { get; set; }
        public string Remarques { get; set; }
        public DossierPatient DossierPatient { get; set; }
       
    }
}
