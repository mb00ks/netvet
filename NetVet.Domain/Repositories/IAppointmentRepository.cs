using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetVet.Domain.Entities;
using NetVet.Domain.Helper;

namespace NetVet.Domain.Repositories
{
    public interface IAppointmentRepository
    {
        /// <summary>
        /// Returns active appointments.
        /// </summary>
        IQueryable<Appointment> GetAppointments(bool includeDeleted = false);
        PagedList<Appointment> GetAppointments(AppointmentParameters appointmentParameters);
    }
}
