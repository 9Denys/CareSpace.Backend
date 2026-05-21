using CareSpace.Backend.API.Common;
using CareSpace.Backend.Application.Services.AvailableService.Commands;
using CareSpace.Backend.Application.Services.AvailableService.Queries;
using CareSpace.Backend.Contracts.DTOs.CreateDTOs;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareSpace.Backend.API.Controllers
{
    [Authorize]
    public class AvailableServiceController : BaseController
    {
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<AvailableServiceDto>>> GetAll()
        {
            var query = new GetAllAvailableServiceQuery();

            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("user/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<AvailableServiceDto>>> GetByUserId(Guid userId)
        {
            var query = new GetAvailableServiceByUserIdQuery(userId);

            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<AvailableServiceDto>> Create(
            [FromBody] CreateAvailableServiceDto dto)
        {
            var command = new CreateAvailableServiceCommand(dto);

            var result = await Mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteAvailableServiceCommand(id);

            await Mediator.Send(command);

            return NoContent();
        }
    }
}