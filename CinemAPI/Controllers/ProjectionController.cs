using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models;
using InputModels.Cinema;
using InputModels.Projection;
using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CinemAPI.Controllers
{
    public class ProjectionController : ApiController
    {
        private readonly INewProjection newProj;
        private readonly IAccessProjection<int> accessProjection;



        public ProjectionController(INewProjection newProj, IAccessProjection<int> accessProjection)
        {
            this.newProj = newProj;
            this.accessProjection = accessProjection;
        }

        [HttpPost]
        public IHttpActionResult Index(ProjectionCreationModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            DateTime parsedDateTime;
            try
            {
                parsedDateTime = DateTime.ParseExact(model.StartDate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            }
            catch
            {
                return BadRequest("DateTime format should be: dd/MM/yyyy HH:mm:ss");
            }
            NewProjectionSummary summary = newProj.New(new Projection(model.MovieId, model.RoomId, parsedDateTime,model.AvailableSeatsCount));


            if (summary.IsCreated)
            {
                return Ok();
            }
            else
            {
                return BadRequest(summary.Message);
            }
        }
        [HttpGet]
        public IHttpActionResult GetAvailableSeatsCount(GetAvailableSeatsModel getAvailableSeatsModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var projection = accessProjection.Access(new Projection() { Id = getAvailableSeatsModel.ProjectionId });
            if (!projection.Exists)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound,projection.Message));
            }
            else if (!projection.ValidDateTime)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Forbidden, projection.Message));
            }
            return Ok(projection.Data);
        }
    }
}