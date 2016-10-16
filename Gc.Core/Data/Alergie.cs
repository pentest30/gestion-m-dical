namespace Gc.Core.Data
{
    public class Allergie
    {
        public int Id { get; set; }
        public int DossierPatientId { get; set; }
        public DossierPatient DossierPatient { get; set; }
    }
}
