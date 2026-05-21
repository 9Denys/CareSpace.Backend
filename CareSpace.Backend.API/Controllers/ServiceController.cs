using CareSpace.Backend.API.Common;
using CareSpace.Backend.Application.Services.Service.Commands;
using CareSpace.Backend.Application.Services.Service.Queries;
using CareSpace.Backend.Contracts.DTOs.CreateDTOs;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using CareSpace.Backend.Contracts.DTOs.UpdateDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareSpace.Backend.API.Controllers
{
    public class ServiceController : BaseController
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<ServiceDto>>> GetAll()
        {
            var query = new GetAllServiceQuery();

            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceDto>> GetById(Guid id)
        {
            var query = new GetServiceByIdQuery(id);

            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceDto>> Create([FromBody] CreateServiceDto dto)
        {
            var command = new CreateServiceCommand(dto);

            var result = await Mediator.Send(command);

            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceDto>> Update(
            Guid id,
            [FromBody] UpdateServiceDto dto)
        {
            var command = new UpdateServiceCommand(id, dto);

            var result = await Mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteServiceCommand(id);

            await Mediator.Send(command);

            return NoContent();
        }
    }
}