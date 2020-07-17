using CinemAPI.Data;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Cinema;
using InputModels.Cinema;
using System.Web.Http;

namespace CinemAPI.Controllers
{
    public class CinemaController : ApiController
    {
        private readonly ICinemaRepository cinemaRepo;

        public CinemaController(ICinemaRepository cinemaRepo)
        {
            this.cinemaRepo = cinemaRepo;
        }

        [HttpPost]
        public IHttpActionResult Index(CinemaCreationModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            ICinema cinema = cinemaRepo.GetByNameAndAddress(model.Name, model.Address);

            if (cinema == null)
            {
                cinemaRepo.Insert(new Cinema(model.Name, model.Address));

                return Ok();
            }

            return BadRequest("Cinema already exists");

        }
    }
}