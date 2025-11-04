using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Post([FromBody] Rental.Rental rental)
        {
	        return Ok(_features.SaveAsync(rental));
        }
	}
}
