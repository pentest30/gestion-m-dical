using System.Collections.ObjectModel;

namespace Gc.Core.Data
{
    public class CompteRendu
    {
        public int Id { get; set; }
        public int ConsultationId { get; set; }
        public string Organe { get; set; }
        public string Observation { get; set; }
        public Consultation Consultation { get; set; }
        public ObservableCollection<string> Organes { get; set; }
    }
}
