using Autofac;
using Gc.Core.Data;
using Gc.Core.DataManage.Core;

namespace Gc.Core
{
    public class AutofacBootStrap
    {
        private static IContainer Container { get; set; }
        public AutofacBootStrap()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DbManager>();
            builder.RegisterType<UnitWork>();
            builder.RegisterType<Repository<DossierPatient>>().As<IRepository<DossierPatient>>();
            builder.RegisterType<Repository<Medicament>>().As<IRepository<Medicament>>();
            builder.RegisterType<Repository<Traitement>>().As<IRepository<Traitement>>();
            builder.RegisterType<Repository<Consultation>>().As<IRepository<Consultation>>();
            builder.RegisterType<Repository<SyptmomeConsult>>().As<IRepository<SyptmomeConsult>>();
            builder.RegisterType<Repository<Symptome>>().As<IRepository<Symptome>>();
            builder.RegisterType<Repository<InterventionChirurgicale>>().As<IRepository<InterventionChirurgicale>>();
            builder.RegisterType<Repository<Antecedent>>().As<IRepository<Antecedent>>();
            builder.RegisterType<Repository<Rendezvou>>().As<IRepository<Rendezvou>>();
            builder.RegisterType<Repository<ArretTravail>>().As<IRepository<ArretTravail>>();
            builder.RegisterType<Repository<Antecedent>>().As<IRepository<Antecedent>>();
            builder.RegisterType<Repository<AnalysesDemande>>().As<IRepository<AnalysesDemande>>();
            builder.RegisterType<Repository<CompteRendu>>().As<IRepository<CompteRendu>>();
            builder.RegisterType<Repository<Dci>>().As<IRepository<Dci>>();
            builder.RegisterType<Repository<SalleAttente>>().As<IRepository<SalleAttente>>();
            builder.RegisterType<Repository<Cabinet>>().As<IRepository<Cabinet>>();
           
            Container = builder.Build();
        }

        public IRepository<SalleAttente> RepositorySalleAtt()
        {
            return Container.Resolve<IRepository<SalleAttente>>();
        }
        public IRepository<Cabinet> RepositoryCabinet()
        {
            return Container.Resolve<IRepository<Cabinet>>();
        }

        public IRepository<Dci> RepositoryDci()
        {
            return Container.Resolve<IRepository<Dci>>();
        }
        public IRepository<CompteRendu> RepositoryCompteRendu()
        {
            return Container.Resolve<IRepository<CompteRendu>>();
        } 
        public IRepository<AnalysesDemande> RepositoryAnaLysesDemandes()
        {
            return Container.Resolve<IRepository<AnalysesDemande>>();
        } 
        public IRepository<Antecedent> AntecedentsRepository()
        {
            return Container.Resolve<IRepository<Antecedent>>();
        } 
        public  IRepository<DossierPatient> ResoleDossierPatientRepo( )
        {
           
            return Container.Resolve<IRepository<DossierPatient>>();
        }

        public IRepository<ArretTravail> ArretTravailRepo()
        {
            return Container.Resolve<IRepository<ArretTravail>>();
        } 
        public IRepository<Medicament> ResoleMedicamentRepo()
        {

            return Container.Resolve<IRepository<Medicament>>();
        }
        public IRepository<Consultation> ResoleConsultRepo()
        {
            return Container.Resolve<IRepository<Consultation>>();
        }
        public IRepository<Traitement> ResoleTraitmentRepo()
        {

            return Container.Resolve<IRepository<Traitement>>();
        }
        public IRepository<SyptmomeConsult> SymptomeConsultRepo()
        {

            return Container.Resolve<IRepository<SyptmomeConsult>>();
        }
        public IRepository<Symptome> SymptomeRepo()
        {

            return Container.Resolve<IRepository<Symptome>>();
        }
        public IRepository<InterventionChirurgicale> AntecedentPatientsRepo()
        {

            return Container.Resolve<IRepository<InterventionChirurgicale>>();
        }
        public IRepository<Antecedent> AntecedentsRepo()
        {

            return Container.Resolve<IRepository<Antecedent>>();
        }

        public IRepository<Rendezvou> RepositoryRendezVous()
        {
            return Container.Resolve<IRepository<Rendezvou>>();
        } 
    }
}
