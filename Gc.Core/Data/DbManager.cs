using System.Data.Entity;

namespace Gc.Core.Data
{
    public class DbManager:DbContext
    {
        public DbManager() : base("PlaningDbContext")
        {
            Configuration.AutoDetectChangesEnabled = false; 
        }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<DossierPatient> DossierPatients { get; set; }
        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<Analyse> Analyses { get; set; }
        public DbSet<Symptome> Symptomes { get; set; }
        public DbSet<InterventionChirurgicale> InterventionChirurgicales { get; set; }
        //public DbSet<Antecedent> Antecedents { get; set; }
        public DbSet<Rendezvou> Rendezvous { get; set; }
        public DbSet<AnalysesDemande> AnalysesDemandes { get; set; }
        public DbSet<SyptmomeConsult> SyptmomeConsults { get; set; }
        public DbSet<Forme> Formes { get; set; }
        public DbSet<Dci> Dcis { get; set; }
        public DbSet<ArretTravail> ArretTravails { get; set; }
        public DbSet<CompteRendu> CompteRendus { get; set; }    
        public DbSet<SalleAttente> SalleAttentes { get; set; }
        public DbSet<Cabinet> Cabinets { get; set; }
    }
}
