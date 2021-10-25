﻿using System;
using System.Linq;
using NetVet.Domain.Entities;
using NetVet.Domain.Helper;

namespace NetVet.Domain.Repositories
{
    public partial class AppointmentRepository : IAppointmentRepository
    {
        IQueryable<Appointment> IAppointmentRepository.GetAppointments( bool includeDeleted)
        {
            IQueryable<Appointment> query = NetVetDbContext.Appointments;
            if (!includeDeleted)
                query = query.Where(x => !x.DateDeleted.HasValue);

            return query;
        }

        PagedList<Appointment> IAppointmentRepository.GetAppointments(AppointmentParameters appointmentParameters)
        {
            throw new NotImplementedException();
        }
    }
}
