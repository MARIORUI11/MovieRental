using Microsoft.AspNetCore.Mvc;
using MovieRental.DTOs;
using MovieRental.Rental;

namespace MovieRental.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentalController : ControllerBase
    {

        private readonly IRentalFeatures _features;

        public RentalController(IRentalFeatures features)
        {
            _features = features;
        }

        [HttpGet("{customerName}")]
        public async Task<IActionResult> GetRentalByCustomerName(string customerName)
        {
            var rentals = await _features.GetRentalsByCustomerName(customerName);

            return rentals.Any() ? Ok(rentals) : NotFound($"No rentals found for customer '{customerName}'"); 
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SaveRentalDto rentalDto)
        {
            var rental = new Rental.Rental
            {
                CustomerId = rentalDto.CustomerId,
                MovieId = rentalDto.MovieId,
                DaysRented = rentalDto.DaysRented,
                RentalCost = rentalDto.RentalCost,
                PaymentMethod = rentalDto.PaymentMethod
            };

            var result = await _features.SaveAsync(rental);
            
            if (result == null)
                return BadRequest("Payment failed.");

            return Ok(result);
        }
	}
}
