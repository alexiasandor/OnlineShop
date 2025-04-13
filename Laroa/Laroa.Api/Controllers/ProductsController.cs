using AutoMapper;
using Laroa.Api.Dtos;
using Laroa.Application;
using Laroa.Domain;
using Laroa.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Laroa.Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly IContentBasedRecommendationService _recommendationService;
        private object _categoryService;

        public ProductsController(IMapper mapper, IProductService productService, IContentBasedRecommendationService recommendationService)
        {
            _mapper = mapper;
            _productService = productService;
            _recommendationService = recommendationService;
        }

        [HttpGet]
        //[Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetAllAsync()
        {
            var products = await _productService.GetAllAsync();

            if (products == null)
            {
                return NotFound();
            }

            var mappedProducts = _mapper.Map<IList<ProductGetDto>>(products);

            return Ok(mappedProducts);
        }

        [HttpGet("{id}")]
        //[Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var mappedProduct = _mapper.Map<ProductGetDto>(product);

            return Ok(mappedProduct);
        }

        [HttpPost]
        //[Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> AddAsync([FromBody] ProductPostDto productPostDto)
        {
            // Validate or set categoryId based on your business logic
            var categoryId = productPostDto.CategoryId;  // You may need to validate or retrieve the category ID based on your requirements

            // Validate the categoryId, assuming it's required
            if (categoryId <= 0)
            {
                return BadRequest("Invalid CategoryId");
            }

            var insertedProduct = await _productService.AddAsync(productPostDto.Name, productPostDto.Description, productPostDto.CategoryId, productPostDto.Price, productPostDto.Stock);

            if (insertedProduct == null)
            {
                return BadRequest();
            }

            return Ok(insertedProduct);
        }


        [HttpDelete("{id}")]
        //[Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deletedProduct = await _productService.DeleteAsync(id);
            return Ok(deletedProduct);
        }


        [HttpPost]
        [Route("{productId}/image")]
        public async Task<IActionResult> AddImageToProduct(int productId, IFormFile File)
        {
            var result = await _productService.AddImageToProductAsync(productId, File, "imaginiproduse");

            if (result == null)
            {
                return null;
            }

            return Ok(result);
        }


        [HttpGet("{id}/recommendations")]
        public async Task<IActionResult> GetRecommendationsAsync(int id)
        {
            var recommendations = await _recommendationService.GetContentBasedRecommendationsAsync(id);

            if (recommendations == null || !recommendations.Any())
            {
                return NotFound("No recommendations found");
            }

            var mappedRecommendations = _mapper.Map<List<ProductGetDto>>(recommendations);

            return Ok(mappedRecommendations);
        }
    }
}
