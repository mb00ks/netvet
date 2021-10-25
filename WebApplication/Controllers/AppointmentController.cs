using Microsoft.AspNetCore.Mvc;
using NetVet.Domain.Entities;
using NetVet.Service;
using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {
        public IAppointmentService AppointmentService { get; set; }

        public AppointmentController(IAppointmentService appointmentService)
        {
            AppointmentService = appointmentService ?? throw new ArgumentNullException("appointmentService");
        }

        // [HttpGet]
        // public IEnumerable<Appointment> Get()
        // {
        //     return AppointmentService.Get().Select(item => new Appointment
        //     {
        //         AppointmentDateTime = item.AppointmentDateTime,
        //         AppointmentId = item.AppointmentId,
        //         PetName = item.Pet.Name,
        //         OwnerName = item.Pet.Owner.FirstName + " " + item.Pet.Owner.LastName,
        //         ContactDetail = item.Pet.Owner.Contacts.FirstOrDefault(find => find.IsPreferred).ContactData
        //     });
        // }

        [HttpGet]
        public IEnumerable<Appointment> Get([FromQuery] AppointmentParameters appointmentParameters)
        {
            var appointment = AppointmentService.Get(appointmentParameters);

            var metadata = new
            {
                appointment.TotalCount,
                appointment.PageSize,
                appointment.CurrentPage,
                appointment.HasNext,
                appointment.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            return appointment.Select(item => new Appointment
            {
                AppointmentDateTime = item.AppointmentDateTime,
                AppointmentId = item.AppointmentId,
                PetName = item.Pet.Name,
                OwnerName = item.Pet.Owner.FirstName + " " + item.Pet.Owner.LastName,
                ContactDetail = item.Pet.Owner.Contacts.FirstOrDefault(find => find.IsPreferred).ContactData
            }).ToArray();
        }
    }
}
