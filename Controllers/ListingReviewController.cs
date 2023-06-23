using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nomad.BusinessLogic.Implementations;
using Nomad.BusinessLogic.Interfaces;
using Nomad.BusinessLogic.Models;

namespace Nomad_v1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingReviewController : ControllerBase
    {
        private readonly IListingReviewService _listingReviewService;

        public ListingReviewController(IListingReviewService listingReviewService)
        {
            _listingReviewService = listingReviewService;
        }

        [HttpGet("get-listing-reviews")]
        public async Task<ActionResult<List<ReviewDetailsModel>>> GetListingReviews(int id)
        {
            try
            {
                var reviews = await _listingReviewService.GetReviewsForListing(id);
                //if (reviews.Count == 0)
                //{
                //    return Ok(0);
                //}
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpGet("get-listing-rating")]
        public async Task<ActionResult<double>> GetListingRating(int id)
        {
            try
            {
                var rating = await _listingReviewService.GetRatingForListing(id);
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

        [HttpPost("add-listing-review")]
        public async Task<ActionResult> AddListingReview(RegisterReviewModel registerReviewModel, int ListingId, int BookingId)
        {
            try
            {
                await _listingReviewService.AddListingReview(registerReviewModel, ListingId, BookingId);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
