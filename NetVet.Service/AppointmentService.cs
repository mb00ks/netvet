using Mehdime.Entity;
using NetVet.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using NetVet.Domain.Entities;
using NetVet.Domain.Helper;

namespace NetVet.Service
{
    public class AppointmentService : IAppointmentService
    {
        //private IDbContextScopeFactory ContextScopeFactory { get; set; }
        private IAppointmentRepository AppointmentRepository { get; set; }

        public AppointmentService(/*IDbContextScopeFactory contextScopeFactory,*/ IAppointmentRepository appointmentRepository)
        {
            //ContextScopeFactory = contextScopeFactory ?? throw new ArgumentNullException("contextScopeFactory");
            AppointmentRepository = appointmentRepository ?? throw new ArgumentNullException("appointmentRepository");
        }

        public List<Appointment> Get()
        {
            return AppointmentRepository.GetAppointments().ToList();
        }

        public PagedList<Appointment> Get(AppointmentParameters appointmentParameters)
        {
            return AppointmentRepository.GetAppointments(appointmentParameters);
        }
    }
}
