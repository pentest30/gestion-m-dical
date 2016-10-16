using System;

namespace Gc.Core.Data
{
    public class SyptmomeConsult
    {
        public int Id { get; set; }
        public Int32 ConsultationId { get; set; }
        public string Diagnostic { get; set; }
        public string Obsevation { get; set; }
        public string Examen { get; set; }
        public Int32? SymptomeId { get; set; }
        public DateTime? Date { get; set; }
        public Consultation Consultation { get; set; }
        public Symptome Symptome { get; set; }
    }
}
