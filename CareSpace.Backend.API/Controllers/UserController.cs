using CareSpace.Backend.API.Common;
using CareSpace.Backend.Application.Services.User.Commands;
using CareSpace.Backend.Application.Services.User.Queries;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using CareSpace.Backend.Contracts.DTOs.UpdateDTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace CareSpace.Backend.API.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetMyProfile()
        {
            var query = new GetCurrentUserQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateMyProfile([FromBody] UserUpdateDto updateDto)
        {
            var command = new UpdateUserCommand(updateDto);
            await Mediator.Send(command);
            return Ok(command);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateMyPassword([FromBody] UserPasswordUpdateDto password)
        {
            var command = new UpdateUserPasswordCommand(password);
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMyProfile()
        {
            var command = new DeleteUserCommand();
            await Mediator.Send(command);
            return NoContent();
        }
    }
}