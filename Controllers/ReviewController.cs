using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nomad.BusinessLogic.Implementations;
using Nomad.BusinessLogic.Interfaces;
using Nomad.BusinessLogic.Models;
using Nomad.DataAccess.Entities;

namespace Nomad_v1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly IListingReviewService _listingReviewService;

        public ReviewController(IReviewService reviewService, IListingReviewService listingReviewService)
        {
            _reviewService = reviewService;
            _listingReviewService = listingReviewService;
        }

        //[HttpGet("userReviews/id")]
        //public async Task<ActionResult<List<ReviewDetailsModel>>> GetReviewsForUser(int id)
        //{
        //    //try
        //    //{
        //        var reviews = await _reviewService.GetReviewsForUser(id);
        //        if(reviews.Count == 0)
        //        {
        //            return Ok(0);
        //        }
        //        return Ok(reviews);
        //    //}
        //    //catch(Exception ex)
        //    //{
        //    //    return Ok(ex.Message);
        //    //}

        //}


        //[HttpGet("listing/ratings")]
        //public async Task<ActionResult<double>> GetListingRating(int id)
        //{
        //    try
        //    {
        //        var rating = await _reviewService.GetRatingForListing(id);
        //        if(rating == 0.00)
        //        {
        //            return Ok("Rating not yet calculated!");
        //        }
        //        return Ok(rating);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpGet("user/ratings")]
        //public async Task<ActionResult<double>> GetUserRating(int id)
        //{
        //    try
        //    {
        //        var rating = await _reviewService.GetRatingForUser(id);
        //        return Ok(rating);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

       

    }
}
