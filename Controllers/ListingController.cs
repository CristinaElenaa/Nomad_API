using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nomad.BusinessLogic.Extensions;
using Nomad.BusinessLogic.Helpers;
using Nomad.BusinessLogic.Implementations;
using Nomad.BusinessLogic.Interfaces;
using Nomad.BusinessLogic.Models;
using Nomad.BussinessLogic.Helpers;
using Nomad.DataAccess.Entities;

namespace Nomad_v1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingController : ControllerBase
    {
        private readonly IListingService _listingService;
        private readonly IPhotoService _photoService;

        public ListingController(IListingService listingService, IPhotoService photoService)
        {
            _listingService = listingService;
            _photoService = photoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ListingModel>>> GetAllListings()
        {
            try
            {
                var data = await _listingService.GetAllListings();
                return data;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("with-photo")]
        public async Task<ActionResult<PagedList<CardListingModel>>> GetAllListingsWithMainPhoto([FromQuery]UserParams userParams)
        {
            try
            {
                var listings = await _listingService.GetAllCardListings(userParams);

                Response.AddPaginationHeader(new PaginationHeader(listings.CurrentPage, listings.PageSize,
                listings.TotalCount, listings.TotalPages));

                return listings;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("listingById")]
        public async Task<ActionResult<ListingModel>> GetListingById(int id)
        {
            try
            {
                var data = await _listingService.GetListingById(id);
                return data;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("host-id")]
        public async Task<ActionResult<IEnumerable<CardListingModel>>> GetListingsForHost(int hostId)
        {
            try
            {
                var result = await _listingService.GetListingsForHost(hostId);
                if(result.Count() == 0)
                {
                    return Ok(Enumerable.Empty<CardListingModel>());
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }

        [HttpGet("search-filters")]
        public async Task<ActionResult<CardListingModel>> SearchByFilters(
            string listingCity, string checkInString, string checkOutString, int capacity)
        {
            try
            {
                var data = await _listingService.SearchListingByFilters(listingCity,
                    checkInString, checkOutString, capacity);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("search")]
        public async Task<ActionResult<List<CardListingModel>>> Search(string listingTitle)
        {
            try
            {
                var listings = await _listingService.SearchListingsByTitle(listingTitle);
                return Ok(listings);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("update-listing")]
        public async Task<ActionResult> UpdateListing(UpdateListingModel updateListingModel)
        {
            try
            {
                await _listingService.UpdateListing(updateListingModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("addListing")]
        public async Task<ActionResult> AddListing([FromForm]RegisterListingModel registerListingModel)
        {
            try
            {
                await _listingService.AddListing(registerListingModel);
                return Ok();
            }
            catch(Exception ex) 
            {
                Console.WriteLine($"An error occurred: {ex.Message}");

                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteListing(int id)
        {
            try
            {
                await _listingService.RemoveListing(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult> AddPhoto(IFormFile file, int listingId)
        {
            try
            {
                var photo = await _photoService.AddListingPhoto(file, listingId);
                return CreatedAtAction(nameof(GetListingById), new { id = listingId }, photo);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


    }
}
