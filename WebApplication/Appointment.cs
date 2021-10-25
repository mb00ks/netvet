using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication
{
    public class Appointment
    {
        public Guid AppointmentId { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string PetName { get; set; }
        public string OwnerName { get; set; }
        public string ContactDetail { get; set; }

    }
}
