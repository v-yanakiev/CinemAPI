using CinemAPI.Data;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Movie;
using InputModels;
using System.Web.Http;
using InputModels.Movie;

namespace CinemAPI.Controllers
{
    public class MovieController : ApiController
    {
        private readonly IMovieRepository movieRepo;

        public MovieController(IMovieRepository movieRepo)
        {
            this.movieRepo = movieRepo;
        }

        [HttpPost]
        public IHttpActionResult Index(MovieCreationModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            IMovie movie = movieRepo.GetByNameAndDuration(model.Name, model.DurationMinutes);

            if (movie == null)
            {
                movieRepo.Insert(new Movie(model.Name, model.DurationMinutes));

                return Ok();
            }

            return BadRequest("Movie already exists");
        }
    }
}