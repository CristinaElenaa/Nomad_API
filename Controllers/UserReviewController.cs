using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nomad.BusinessLogic.Implementations;
using Nomad.BusinessLogic.Interfaces;
using Nomad.BusinessLogic.Models;

namespace Nomad_v1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserReviewController : ControllerBase
    {
        private readonly IUserReviewService _userReviewService;

        public UserReviewController(IUserReviewService userReviewService)
        {
            _userReviewService = userReviewService;
        }

        [HttpGet("get-user-reviews")]
        public async Task<ActionResult<List<ReviewDetailsModel>>> GetUserReviews(int id)
        {
            try
            {

                var reviews = await _userReviewService.GetReviewsForUser(id);
                if (reviews.Count == 0)
                {
                    return Ok(0);
                }
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpGet("get-user-rating")]
        public async Task<ActionResult<double>> GetUserRating(int id)
        {
            try
            {
                var rating = await _userReviewService.GetRatingForUser(id);
                if (rating == 0.00)
                {
                    return Ok("Rating not yet calculated!");
                }
                return Ok(rating);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-user-review")]
        public async Task<ActionResult> AddUserReview(RegisterReviewModel registerReviewModel, int userId, int BookingId)
        {
            try
            {
                await _userReviewService.AddUserReview(registerReviewModel, userId, BookingId);
                return Ok();

            }
            catch (Exception ex)
            {
                Exception innerException = ex.InnerException;
                string errorMessage = innerException.Message;

                // Log or display the error message
                Console.WriteLine("Error: " + errorMessage);
                return BadRequest(errorMessage);
            }
        }
    }
}
