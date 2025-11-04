using Microsoft.EntityFrameworkCore;
using MovieRental.Data;
using MovieRental.DTOs;

namespace MovieRental.Movie
{
	public class MovieFeatures : IMovieFeatures
	{
		private readonly MovieRentalDbContext _movieRentalDb;
		public MovieFeatures(MovieRentalDbContext movieRentalDb)
		{
			_movieRentalDb = movieRentalDb;
		}
		
		public Movie Save(Movie movie)
		{
			_movieRentalDb.Movies.Add(movie);
			_movieRentalDb.SaveChanges();
			return movie;
		}

		// TODO: tell us what is wrong in this method? Forget about the async, what other concerns do you have?
		public async Task<MovieResponseDto> GetAll(
			bool onlyActive,
            int pageSize,
			int pageNumber)
		{
			try
			{
				if (pageSize <= 0 || pageNumber < 0)
				{
					throw new ArgumentException("Page size must be greater than 0 and page number cannot be negative.");
                }

				var query = _movieRentalDb.Movies.AsQueryable();

                if (onlyActive)
                {
                    query = query.Where(m => m.IsActive);
                }

				var totalMovies = await query.CountAsync();
                var totalPages = (int)Math.Ceiling(totalMovies / (double)pageSize);

                var movieList = await query
					.OrderBy(t => t.Title)
					.Skip(pageNumber * pageSize)
					.Take(pageSize)
					.Select(m => new MovieDto
					{
						Name = m.Title,
						IsActive = m.IsActive
					})
                    .ToListAsync();

                return new MovieResponseDto
				{
					PageSize = pageSize,
                    PageNumber = pageNumber,
					TotalPages = totalPages,
                    TotalMovies = totalMovies,
					Movies = movieList
                };
            }
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred while retrieving movies: {ex.Message}");
				throw;
			}
		}
	}
}
