using System;
using System.Collections.Generic;
using System.ComponentModel;
using Gc.Core.Data;

namespace GC.MV
{
    public class RendeVousCollectionView
    {
        public RendeVousCollectionView(IEnumerable<Rendezvou> rendezvous )
        {
            Rendezvous = new BindingList<RendezVousMv>();
            foreach (var rendezvous1 in rendezvous)
            {
                var  item =  new RendezVousMv();
                item.Id = rendezvous1.Id;
                item.PatientId = rendezvous1.PatientId;
                item.Description = rendezvous1.Description;
                item.Subject = rendezvous1.Patient.NomComplet ;
                if (rendezvous1.HourFrom != null) item.StartTime = (DateTime) rendezvous1.HourFrom;
                if (rendezvous1.HourTo != null) item.EndTime = rendezvous1.HourTo.Value;
                Rendezvous.Add(item);
            }
        }
        public BindingList<RendezVousMv> Rendezvous { get; set; }

    }
}
