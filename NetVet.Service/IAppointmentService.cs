using NetVet.Domain.Entities;
using NetVet.Domain.Helper;
using System.Collections.Generic;

namespace NetVet.Service
{
    public interface IAppointmentService
    {
        List<Appointment> Get();
        PagedList<Appointment> Get(AppointmentParameters appointmentParameters);
    }
}