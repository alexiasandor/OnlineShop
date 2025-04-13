using AutoMapper;
using Laroa.Domain.Interfaces.Services;
using Laroa.Api.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Laroa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService, IMapper mapper)
        {
            _mapper = mapper;
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var reviews = await _reviewService.GetAllAsync();
            if (reviews == null)
            {
                return NotFound();
            }

            var mappedReviews = _mapper.Map<IList<ReviewGetDto>>(reviews);

            return Ok(mappedReviews);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var review = await _reviewService.GetByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            var mappedReview = _mapper.Map<ReviewGetDto>(review);

            return Ok(mappedReview);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] ReviewPostDto reviewPostDto)
        {
            var insertedReview = await _reviewService.AddAsync(reviewPostDto.ProductId, reviewPostDto.UserId, reviewPostDto.Comment, reviewPostDto.UserName);
            if (insertedReview == null)
            {
                return BadRequest();
            }
            return Ok(insertedReview);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deletedReview = await _reviewService.DeleteAsync(id);
            return Ok(deletedReview);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateReview(int ReviewId, string Comment)
        {
            var updatedReview = await _reviewService.UpdateAsync(ReviewId, Comment);

            if (updatedReview == null)
            {
                return NotFound();
            }

            return Ok(updatedReview);
        }
    }
}
