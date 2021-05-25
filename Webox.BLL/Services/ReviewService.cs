using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webox.BLL.DTO;
using Webox.BLL.Interfaces;
using Webox.DAL.Entities;
using Webox.DAL.Interfaces;

namespace Webox.BLL.Services
{
    public class ReviewService : IReviewService
    {
        private readonly UserManager<UserAccount> userManager;
        private readonly IUnitOfWork unitOfWork;

        public ReviewService(UserManager<UserAccount> userManager, IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<ReviewInfoDTO>> GetReviews(string userName)
        {
            var userAccount = await userManager.FindByNameAsync(userName);
            var userReviews = await unitOfWork.UserAccount.GetReviews(userAccount.Id);
            var reviews = new List<ReviewInfoDTO>();
            foreach (var review in userReviews)
            {
                reviews.Add(new ReviewInfoDTO
                {
                    Id = review.ReviewId,
                    PubDateTime = review.PublishDateTime,
                    Rating = review.Rating,
                    Text = review.ReviewText,
                    UserName = $"{userAccount.FirstName} {userAccount.LastName}",
                    LaptopId = review.LaptopId
                });
            }
            return reviews;
        }

        public async Task<ReviewInfoDTO> GetReviewById(string reviewId)
        {
            var review = await unitOfWork.Reviews.GetById(reviewId);
            var fullName = await unitOfWork.UserAccount.GetFullName(review.AccountId);
            return new ReviewInfoDTO 
            {
                Id = review.ReviewId,
                PubDateTime = review.PublishDateTime,
                Rating = review.Rating,
                Text = review.ReviewText,
                UserName = fullName,
                LaptopId = review.LaptopId
            };
        }

        private async Task UpdateLaptopInfo(string laptopId)
        {
            var rating = (await unitOfWork.Reviews.GetAll())
                .Where(r => r.LaptopId.Equals(laptopId))
                .Average(r => r.Rating);
            var laptop = await unitOfWork.Laptops.GetById(laptopId);
            laptop.Rating = rating;
            await unitOfWork.Laptops.Update(laptop);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task SaveReview(ReviewDTO data, string userName)
        {
            await unitOfWork.Reviews.Add(new Review 
            {
                ReviewText = data.ReviewText,
                PublishDateTime = DateTime.UtcNow,
                Rating = data.Rating,
                AccountId = (await userManager.FindByNameAsync(userName)).Id,
                LaptopId = data.LaptopId,
            });
            await unitOfWork.SaveChangesAsync();
            await UpdateLaptopInfo(data.LaptopId);
        }

        public async Task UpdateReview(ReviewDTO data, string reviewId)
        {
            var entity = await unitOfWork.Reviews.GetById(reviewId);
            entity.ReviewText = data.ReviewText;
            entity.PublishDateTime = DateTime.UtcNow;
            entity.Rating = data.Rating;
            await unitOfWork.Reviews.Update(entity);
            await unitOfWork.SaveChangesAsync();
            await UpdateLaptopInfo(data.LaptopId);
        }

        public async Task DeleteReview(string reviewId)
        {
            var entity = await unitOfWork.Reviews.GetById(reviewId);
            var laptopId = entity.LaptopId;
            await unitOfWork.Reviews.Delete(reviewId);
            await unitOfWork.SaveChangesAsync();
            await UpdateLaptopInfo(laptopId);
        }
    }
}
