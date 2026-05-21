using CareSpace.Backend.API.Common;
using CareSpace.Backend.Application.Services.TimeSlot.Commands;
using CareSpace.Backend.Application.Services.TimeSlot.Queries;
using CareSpace.Backend.Contracts.DTOs.CreateDTOs;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using CareSpace.Backend.Contracts.DTOs.UpdateDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareSpace.Backend.API.Controllers
{
    public class TimeSlotController : BaseController
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<TimeSlotDto>>> GetAll()
        {
            var query = new GetAllTimeSlotQuery();

            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<TimeSlotDto>> GetById(Guid id)
        {
            var query = new GetTimeSlotByIdQuery(id);

            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TimeSlotDto>> Create(
            [FromBody] CreateTimeSlotDto dto)
        {
            var command = new CreateTimeSlotCommand(dto);

            var result = await Mediator.Send(command);

            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TimeSlotDto>> Update(
            Guid id,
            [FromBody] UpdateTimeSlotDto dto)
        {
            var command = new UpdateTimeSlotCommand(id, dto);

            var result = await Mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteTimeSlotCommand(id);

            await Mediator.Send(command);

            return NoContent();
        }
    }
}