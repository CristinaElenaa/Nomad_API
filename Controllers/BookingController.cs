using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nomad.BusinessLogic.Interfaces;
using Nomad.BusinessLogic.Models;

namespace Nomad_v1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDetailsModel>>> GetAllBookings()
        {
            try
            {
                var data = await _bookingService.GetAll();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("listingId")]
        public async Task<ActionResult<IEnumerable<BookingDetailsModel>>> GetAllBookingsForListing(int listingId)
        {
            try
            {
                var data = await _bookingService.GetAllByListingId(listingId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("occupied-dates")]
        public async Task<ActionResult<IEnumerable<DateTime>>> GetOccupiedDatesForListing(int listingId)
        {
            try
            {
                var data = await _bookingService.GetOccupiedDates(listingId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("userId")]
        public async Task<ActionResult<IEnumerable<BookingDetailsModel>>> GetAllBookingsForUser(int userId)
        {
            try
            {
                var data = await _bookingService.GetAllByUserId(userId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult> AddBooking(RegisterBookingModel registerBookingModel)
        {
            try
            {
               await _bookingService.Add(registerBookingModel);
                return Ok();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
