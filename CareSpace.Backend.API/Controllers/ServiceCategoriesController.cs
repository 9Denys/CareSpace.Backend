using CareSpace.Backend.API.Common;
using CareSpace.Backend.Application.Services.ServiceCategory.Commands;
using CareSpace.Backend.Application.Services.ServiceCategory.Queries;
using CareSpace.Backend.Contracts.DTOs.CreateDTOs;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using CareSpace.Backend.Contracts.DTOs.UpdateDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareSpace.Backend.API.Controllers
{
    public class ServiceCategoryController : BaseController
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<ServiceCategoryDto>>> GetAll()
        {
            var query = new GetAllServiceCategoryQuery();

            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceCategoryDto>> GetById(Guid id)
        {
            var query = new GetServiceCategoryByIdQuery(id);

            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceCategoryDto>> Create(
            [FromBody] CreateServiceCategoryDto dto)
        {
            var command = new CreateServiceCategoryCommand(dto);

            var result = await Mediator.Send(command);

            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceCategoryDto>> Update(
            Guid id,
            [FromBody] UpdateServiceCategoryDto dto)
        {
            var command = new UpdateServiceCategoryCommand(id, dto);

            var result = await Mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteServiceCategoryCommand(id);

            await Mediator.Send(command);

            return NoContent();
        }
    }
}