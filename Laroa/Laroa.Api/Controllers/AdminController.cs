using AutoMapper;
using Laroa.Api.Dtos;
using Laroa.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Laroa.Api.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAdminService _adminService;

        public AdminController(IMapper mapper, IAdminService adminService)
        {
            _mapper = mapper;
            _adminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var admin = await _adminService.GetAllAsync();

            if (admin == null)
            {
                return NotFound();
            }

            var mappedAdmin = _mapper.Map<IList<AdminGetDto>>(admin);

            return Ok(mappedAdmin);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var admin = await _adminService.GetByIdAsync(id);

            if (admin == null)
            {
                return NotFound();
            }

            var mappedAdmin = _mapper.Map<AdminGetDto>(admin);

            return Ok(mappedAdmin);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] AdminPostDto adminPostDto)
        {
            var insertedAdmin = await _adminService.AddAsync(adminPostDto.Nume, adminPostDto.Prenume, adminPostDto.Email, adminPostDto.Permisiuni);

            if (insertedAdmin == null)
            {
                return BadRequest();
            }

            return Ok(insertedAdmin);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deletedAdmin = await _adminService.DeleteAsync(id);
            return Ok(deletedAdmin);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateAsync(int id, string nume, string prenume, string email, string permisiuni)
        {
            var updatedAdmin = await _adminService.UpdateAsync(id, nume, prenume, email, permisiuni);

            if (updatedAdmin == null)
            {
                return NotFound();
            }

            return Ok(updatedAdmin);
        }



    }
}
