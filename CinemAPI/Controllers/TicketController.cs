using CinemAPI.Domain.Contracts;
using InputModels.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CinemAPI.Controllers
{
    public class TicketController : ApiController
    {
        private readonly IBuyTicket buyTicket;

        public TicketController(IBuyTicket buyTicket)
        {
            this.buyTicket = buyTicket;
        }
        [HttpPost]
        public IHttpActionResult BuyTicket(TicketBuyingModel ticketBuyingModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var result = buyTicket.BuyWithoutReservation(ticketBuyingModel);
            if (result.BoughtSuccessfully)
            {
                return Ok(result.TicketReceipt);
            }
            return ResponseMessage(Request.CreateResponse(result.StatusCode, result.Message));

        }
        [HttpPost]
        public IHttpActionResult BuyTicketWithReservation(TicketWithReservationBuyingModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var result = buyTicket.BuyWithReservation(model.ReservationId);
            if (result.BoughtSuccessfully)
            {
                return Ok(result.TicketReceipt);
            }
            return ResponseMessage(Request.CreateResponse(result.StatusCode, result.Message));

        }


    }
}
