using CinemAPI.Domain.Contracts;
using InputModels.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CinemAPI.Controllers
{
    public class ReservationController : ApiController
    {
        private readonly INewReservation newReservation;
        private readonly ICancelReservation accessProjection;


        public ReservationController(INewReservation newReservation, ICancelReservation accessProjection)
        {
            this.newReservation = newReservation;
            this.accessProjection = accessProjection;
        }
        [HttpPost]
        public IHttpActionResult CreateReservation(ReservationCreationModel reservationCreationModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var result = newReservation.Create(reservationCreationModel);
            if (result.CreatedSuccessfully)
            {
                return Ok(result.ReservationTicket);
            }
            return ResponseMessage(Request.CreateResponse(result.StatusCode, result.Message));

        }
        [HttpPost]
        public IHttpActionResult CancelReservation(ReservationCancellationModel reservationCancellationModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var result = accessProjection.Cancel(reservationCancellationModel.ReservationId);
            if (result.Success)
            {
                return Ok();
            }
            return ResponseMessage(Request.CreateResponse(result.StatusCode, result.Message));
        }
    }
}
