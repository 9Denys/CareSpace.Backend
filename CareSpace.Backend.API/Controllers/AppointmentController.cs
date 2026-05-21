using CareSpace.Backend.API.Common;
using CareSpace.Backend.Application.Services.Appointment.Commands;
using CareSpace.Backend.Application.Services.Appointment.Queries;
using CareSpace.Backend.Contracts.DTOs.CreateDTOs;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareSpace.Backend.API.Controllers
{
    [Authorize]
    public class AppointmentController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<AppointmentDto>> Create(
            [FromBody] CreateAppointmentDto dto)
        {
            var command = new CreateAppointmentCommand(dto);

            var result = await Mediator.Send(command);

            return Ok(result);
        }

        [HttpGet("my")]
        public async Task<ActionResult<List<AppointmentDto>>> GetMy()
        {
            var query = new GetMyAppointmentQuery();

            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpPatch("{id}/cancel")]
        public async Task<ActionResult<AppointmentDto>> Cancel(Guid id)
        {
            var command = new CancelAppointmentCommand(id);

            var result = await Mediator.Send(command);

            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<AppointmentDto>>> GetAll()
        {
            var query = new GetAllAppointmentQuery();

            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<AppointmentDto>> GetById(Guid id)
        {
            var query = new GetAppointmentByIdQuery(id);

            var result = await Mediator.Send(query);

            return Ok(result);
        }
    }
}