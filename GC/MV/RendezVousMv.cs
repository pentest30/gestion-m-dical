using System;
using Gc.Core.Data;

namespace GC.MV
{
    public class RendezVousMv
    {
        DateTime start;
        DateTime end;
        string subject;
        int status;
        string description;
        long label;
        string location;
        bool allday;
        int eventType;
        string recurrenceInfo;
        string reminderInfo;
       
        public long Id { get; set; }

       
        public int PatientId { get; set; }
      
        public DateTime StartTime { get { return start; } set { start = value; } }
        public DateTime EndTime { get { return end; } set { end = value; } }
        public string Subject { get { return subject; } set { subject = value; } }
        public int Status { get { return status; } set { status = value; } }
        public string Description { get { return description; } set { description = value; } }
        public long Label { get { return label; } set { label = value; } }
        public string Location { get { return location; } set { location = value; } }
        public bool AllDay { get { return allday; } set { allday = value; } }
        public int EventType { get { return eventType; } set { eventType = value; } }
        public string RecurrenceInfo { get { return recurrenceInfo; } set { recurrenceInfo = value; } }
        public string ReminderInfo { get { return reminderInfo; } set { reminderInfo = value; } }
        public DossierPatient Patient { get; set; }
    }
}
