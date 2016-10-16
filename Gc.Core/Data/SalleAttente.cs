using System;

namespace Gc.Core.Data
{
    public class SalleAttente
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public int PatientId { get; set; }
        public DateTime DateTime { get; set; }
        public DossierPatient Patient { get; set; }

    }
}
