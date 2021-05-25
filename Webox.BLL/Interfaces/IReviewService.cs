using System.Collections.Generic;
using System.Threading.Tasks;
using Webox.BLL.DTO;

namespace Webox.BLL.Interfaces
{
    public interface IReviewService
    {
        Task<List<ReviewInfoDTO>> GetReviews(string userName);
        Task<ReviewInfoDTO> GetReviewById(string reviewId);
        Task SaveReview(ReviewDTO data, string userName);
        Task UpdateReview(ReviewDTO data, string reviewId);
        Task DeleteReview(string reviewId);
    }
}
