using System.ComponentModel.DataAnnotations;

namespace MovieRental.Movie
{
	public class Movie
	{
		[Key]
		public int Id { get; set; }
		
		[Required]
        public string Title { get; set; } = string.Empty;

		public bool IsActive { get; set; } = true;

    }
}
