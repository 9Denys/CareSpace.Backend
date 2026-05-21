using CareSpace.Backend.API.Common;
using CareSpace.Backend.Application.Services.ServiceCentre.Commands;
using CareSpace.Backend.Application.Services.ServiceCentre.Queries;
using CareSpace.Backend.Contracts.DTOs.CreateDTOs;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using CareSpace.Backend.Contracts.DTOs.UpdateDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareSpace.Backend.API.Controllers
{
    public class ServiceCentreController : BaseController
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<ServiceCentreDto>>> GetAll()
        {
            var query = new GetAllServiceCentreQuery();

            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceCentreDto>> GetById(Guid id)
        {
            var query = new GetServiceCentreByIdQuery(id);

            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceCentreDto>> Create(
            [FromBody] CreateServiceCentreDto dto)
        {
            var command = new CreateServiceCentreCommand(dto);

            var result = await Mediator.Send(command);

            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceCentreDto>> Update(
            Guid id,
            [FromBody] UpdateServiceCentreDto dto)
        {
            var command = new UpdateServiceCentreCommand(id, dto);

            var result = await Mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteServiceCentreCommand(id);

            await Mediator.Send(command);

            return NoContent();
        }
    }
}