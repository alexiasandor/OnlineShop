using AutoMapper;
using Laroa.Api.Dtos;
using Laroa.Application;
using Laroa.Domain;
using Laroa.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Laroa.Api.Controllers
{

    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterUserDto registerUserDto)
        {
            var registerAdminResponse = await _userService
                .RegisterAsync(registerUserDto.Name, registerUserDto.Birthday, registerUserDto.Email, registerUserDto.Password, true);

            if (registerAdminResponse.Success == false)
            {
                return BadRequest(registerAdminResponse);
            }

            return Ok(registerAdminResponse);
        }

        [HttpPost]
        [Route("register-client")]
        public async Task<IActionResult> RegisterClient([FromBody] RegisterUserDto registerUserDto)
        {
            var registerClientResponse = await _userService
                .RegisterAsync(registerUserDto.Name, registerUserDto.Birthday, registerUserDto.Email, registerUserDto.Password, false);

            if (registerClientResponse.Success == false)
            {
                return BadRequest(registerClientResponse);
            }

            
            return Ok(registerClientResponse);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            var loginResponse = await _userService.LoginAsync(loginUserDto.Email, loginUserDto.Password);

            if (loginResponse.Success == false)
            {
                return Unauthorized(loginResponse);
            }

            return Ok(loginResponse);
        }

        [HttpPatch]
        [Route("update-client/{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] PatchClientDto patchClientDto)
        {
            var updatedClient = await _userService
                .UpdateClientAsync(id, patchClientDto.Name, patchClientDto.Email);

            if (updatedClient == null)
            {
                return NotFound();
            }

            return Ok(updatedClient);
        }


        [HttpDelete]
        [Route("delete-client/{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var deletedClient = await _userService.DeleteClientAsync(id);

            if (deletedClient == null)
            {
                return NotFound();
            }

            return Ok(deletedClient);
        }

        [HttpGet]
        [Route("get-clients")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetClients()
        {
            var clients = await _userService.GetClientsAsync();

            if (clients == null)
            {
                return NotFound();
            }

            return Ok(clients);
        }
    }
}
