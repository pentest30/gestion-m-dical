using System;
using System.ComponentModel;
using Gc.Core.Annotations;

namespace Gc.Core.Data
{
    public class Traitement
    {
        public Int32 Id { get; set; }
        public int ConsultationId { get; set; }
        public int MedicamentId { get; set; }
        public int Qnt { get; set; }
        public int NbrJours { get; set; }
        public Consultation Consultation { get; set; }
        public string Description { get; set; }
       
        public Medicament Medicament { get; set; }
        
       // public Medicament Medicament { get; set; }
       
    }
}
