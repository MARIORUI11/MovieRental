using Microsoft.EntityFrameworkCore;
using MovieRental.Data;
using MovieRental.PaymentProviders;

namespace MovieRental.Rental
{
	public class RentalFeatures : IRentalFeatures
	{
		private readonly MovieRentalDbContext _movieRentalDb;
		private readonly IEnumerable<IPaymentProvider> _paymentProviders;

        public RentalFeatures(
			MovieRentalDbContext movieRentalDb,
			IEnumerable<IPaymentProvider> paymentProviders)
		{
			_movieRentalDb = movieRentalDb;
			_paymentProviders = paymentProviders;
        }

		//TODO: make me async :(
		//DONE: Made Async :)
		public async Task<Rental> SaveAsync(Rental rental)
		{
            if (rental.PaymentMethod == null)
				throw new Exception("Payment method is required");

            var rentalPaymentProvider = GetProviderByName(rental.PaymentMethod);

			if (rentalPaymentProvider == null)
				throw new Exception($"Payment method {rental.PaymentMethod} not supported");

			bool paymentResult = false;

			try
			{
				paymentResult = await rentalPaymentProvider.Pay(999.99);
			}
			catch (Exception ex)
			{
				throw new Exception($"Payment processing failed for method {rental.PaymentMethod}", ex);
            }

			if (!paymentResult)
				throw new Exception($"Payment processing failed for method {rental.PaymentMethod}");

            await _movieRentalDb.Rentals.AddAsync(rental);
			await _movieRentalDb.SaveChangesAsync();
			return rental;
		}

		//TODO: finish this method and create an endpoint for it
		public async Task<IEnumerable<Rental>> GetRentalsByCustomerName(string customerName)
		{
			var rentalByCustomerName =  await _movieRentalDb.Rentals
				.Include(r => r.Customer)
				.Where(r => r.Customer != null && r.Customer.Name.Equals(customerName))
				.ToListAsync();

            return rentalByCustomerName;
		}

        private IPaymentProvider? GetProviderByName(string? paymentMethod)
        {
            return paymentMethod.ToLower() switch
            {
                "mbway" => _paymentProviders.OfType<MbWayProvider>().FirstOrDefault(),
                "paypal" => _paymentProviders.OfType<PayPalProvider>().FirstOrDefault(),
                _ => null
            };
        }
    }
}
