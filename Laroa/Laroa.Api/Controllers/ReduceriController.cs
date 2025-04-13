using AutoMapper;
using Laroa.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Laroa.Api.Dtos;
using Laroa.Application;
using Laroa.Domain;

namespace Laroa.Api.Controllers
{
    [Route("api/reduceri")]
    [ApiController]
    public class ReduceriController : ControllerBase
    {
        private readonly IReduceriService _reduceriService;
        private readonly IMapper _mapper;

        public ReduceriController(IReduceriService reduceriService , IMapper mapper)
        {
            _reduceriService = reduceriService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var reduceri = await _reduceriService.GetAllAsync();
            return Ok(reduceri);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
           var  reducere = await _reduceriService.GetByIdAsync(id);
           if (reducere == null)
            {
                return NotFound();
            }
            var mappedReducere = _mapper.Map<ReducereGetDto>(reducere);

            return Ok(mappedReducere);
        }
        

        [HttpPost] 
        public async Task<IActionResult> AddAsync([FromBody ] ReduceriPostDto reduceriPostDto)
        {
            var insertedReducere = await _reduceriService.AddAsync(reduceriPostDto.Perioada, reduceriPostDto.Procent, reduceriPostDto.Tip);
            if(insertedReducere == null)
            {
                return BadRequest();

            }

            return Ok(insertedReducere);
        }
        [HttpPost]
        [Route("add-reducere-to-product/{reducereId}/{productId}")]
        public async Task<IActionResult> AddReducereToProduct(int reducereId, int productId)
        {
            var reducere = await _reduceriService.AddReduceriToProduct(reducereId, productId);
            if(reducere == null)
            {
                return NotFound();

            }

            var mappedReducere = _mapper.Map<ReducereGetDto>(reducere);
            return Ok(mappedReducere);
        }
       
        
        [HttpDelete("{id}")]
        public async Task <IActionResult> DeleteAsync(int id)
        {
            var deleteReducere = await _reduceriService.DeleteAsync(id);
            return Ok(deleteReducere);
        }
        [HttpDelete]
        [Route("delete-reducere-from-product/{reducereId}/{productId}")]

        public async  Task<IActionResult> RemoveReducereFromProduct(int reducereId, int productId)
        {
            var reducere = await _reduceriService.RemoveProductFromReduceri(reducereId, productId);
            if (reducere == null)
            {
                return NotFound();
            }

            var mappedOrder = _mapper.Map<ReducereGetDto>(reducere);
            return Ok(mappedOrder);
        }
       

        [HttpPatch]
        public async Task<IActionResult> UpdateReducere(int reducereId, DateTime? Perioada, float Procent, string tip)
        {
            var updateReducere = await _reduceriService.UpdateAsync(reducereId, Perioada, Procent, tip);

            if(updateReducere == null)
            {
                return NotFound();
            }

            return Ok(updateReducere);  
        }
    }
}
