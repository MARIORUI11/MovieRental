using Microsoft.AspNetCore.Mvc;
using MovieRental.Movie;

namespace MovieRental.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {

        private readonly IMovieFeatures _features;

        public MovieController(IMovieFeatures features)
        {
            _features = features;
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            bool onlyActive,
            int? pageSize,
            int? pageNumber)
        {
            var movies = await _features.GetAll(
                onlyActive: onlyActive,
                pageSize: pageSize ?? 10,
                pageNumber: pageNumber ?? 0);

            return Ok(movies);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Movie.Movie movie)
        {
	        return Ok(_features.Save(movie));
        }
    }
}
