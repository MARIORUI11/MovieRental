namespace MovieRental.DTOs
{
    public class SaveRentalDto
    {
        public int CustomerId { get; set; }
        public int MovieId { get; set; }
        public int DaysRented { get; set; }
        public decimal RentalCost { get; set; }
        public string? PaymentMethod { get; set; }
    }
}
