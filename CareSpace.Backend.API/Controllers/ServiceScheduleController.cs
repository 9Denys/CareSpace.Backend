using CareSpace.Backend.API.Common;
using CareSpace.Backend.Application.Services.ServiceSchedule.Commands;
using CareSpace.Backend.Application.Services.ServiceSchedule.Queries;
using CareSpace.Backend.Contracts.DTOs.CreateDTOs;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using CareSpace.Backend.Contracts.DTOs.UpdateDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareSpace.Backend.API.Controllers
{
    public class ServiceScheduleController : BaseController
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<ServiceScheduleDto>>> GetAll()
        {
            var query = new GetAllServiceScheduleQuery();

            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("available")]
        [AllowAnonymous]
        public async Task<ActionResult<List<ServiceScheduleDto>>> GetAvailable(
            [FromQuery] Guid serviceId,
            [FromQuery] Guid centreId,
            [FromQuery] DateOnly date)
        {
            var query = new GetAvailableServiceSchedulesQuery(
                serviceId,
                centreId,
                date
            );

            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceScheduleDto>> GetById(Guid id)
        {
            var query = new GetServiceScheduleByIdQuery(id);

            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceScheduleDto>> Create(
            [FromBody] CreateServiceScheduleDto dto)
        {
            var command = new CreateServiceScheduleCommand(dto);

            var result = await Mediator.Send(command);

            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceScheduleDto>> Update(
            Guid id,
            [FromBody] UpdateServiceScheduleDto dto)
        {
            var command = new UpdateServiceScheduleCommand(id, dto);

            var result = await Mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteServiceScheduleCommand(id);

            await Mediator.Send(command);

            return NoContent();
        }
    }
}