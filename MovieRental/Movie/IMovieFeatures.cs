using MovieRental.DTOs;

namespace MovieRental.Movie;

public interface IMovieFeatures
{
	Movie Save(Movie movie);
	Task<MovieResponseDto> GetAll(bool onlyActive, int pageSize, int pageNumber);
}