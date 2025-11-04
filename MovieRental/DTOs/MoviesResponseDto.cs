namespace MovieRental.DTOs
{
    public class MovieResponseDto
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int TotalMovies { get; set; }
        public List<MovieDto> Movies { get; set; } = new();
    }
}
