using InputModels.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Domain.Contracts.Models
{
    public class NewReservationSummary
    {
        public bool CreatedSuccessfully { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
        public string Message { get; private set; }
        public ReservationTicketModel ReservationTicket { get; private set; }

        public NewReservationSummary(bool createdSuccessfully, HttpStatusCode statusCode, string message, ReservationTicketModel reservationTicket)
        {
            CreatedSuccessfully = createdSuccessfully;
            StatusCode = statusCode;
            Message = message;
            ReservationTicket = reservationTicket;
        }
    }
}
