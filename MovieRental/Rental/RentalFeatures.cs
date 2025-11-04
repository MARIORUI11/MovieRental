using Microsoft.EntityFrameworkCore;
using MovieRental.Data;
using System.Threading.Tasks;

namespace MovieRental.Rental
{
	public class RentalFeatures : IRentalFeatures
	{
		private readonly MovieRentalDbContext _movieRentalDb;
		public RentalFeatures(MovieRentalDbContext movieRentalDb)
		{
			_movieRentalDb = movieRentalDb;
		}

		//TODO: make me async :(
		//DONE: Made Async :)
		public async Task<Rental> SaveAsync(Rental rental)
		{
            await _movieRentalDb.Rentals.AddAsync(rental);
			await _movieRentalDb.SaveChangesAsync();
			return rental;
		}

		//TODO: finish this method and create an endpoint for it
		public async Task<IEnumerable<Rental>> GetRentalsByCustomerName(string customerName)
		{
			var rentalByCustomerName =  _movieRentalDb.Rentals
				.Where(r => r.CustomerName.Equals(customerName));

            return rentalByCustomerName;
		}

	}
}
