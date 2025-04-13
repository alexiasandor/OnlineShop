using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laroa.Domain;
using Laroa.Domain.Interfaces.Repositories;
using Laroa.Domain.Interfaces.Services;

namespace Laroa.Application
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Review> AddAsync(int prodId, int userId, string comment, string userName)
        {
            var review = new Review
            {
                ProductId = prodId,
                UserId = userId,
                Comment = comment,
                UserName = userName
            };

            await _unitOfWork.ReviewRepository.AddAsync(review);
            await _unitOfWork.Save();

            return review;
        }

        public async Task<Review> DeleteAsync(int id)
        {
            var searchedReview = await _unitOfWork.ReviewRepository.GetByIdAsync(id);

            if (searchedReview == null)
                return null;

            await _unitOfWork.ReviewRepository.DeleteAsync(searchedReview);
            await _unitOfWork.Save();

            return searchedReview;
        }

        public async Task<IList<Review>> GetAllAsync()
        {
            return await _unitOfWork.ReviewRepository.GetAllAsync();
        }

        public async Task<Review> GetByIdAsync(int id)
        {
            return await _unitOfWork.ReviewRepository.GetByIdAsync(id);
        }

        public async Task<Review> UpdateAsync(int id, string comment)
        {
            var searchedReview = await _unitOfWork.ReviewRepository.GetByIdAsync(id);

            if (searchedReview == null)
                return null;

            searchedReview.Comment = comment ?? searchedReview.Comment;

            await _unitOfWork.Save();

            return searchedReview;
        }
    }
}
