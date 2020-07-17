using InputModels.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Domain.Contracts.Models
{
    public class BuyTicketSummary
    {
        public BuyTicketSummary(bool boughtSuccessfully, HttpStatusCode statusCode, string message, TicketReceiptModel ticketReceipt)
        {
            BoughtSuccessfully = boughtSuccessfully;
            StatusCode = statusCode;
            Message = message;
            TicketReceipt = ticketReceipt;
        }

        public bool BoughtSuccessfully { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
        public string Message { get; private set; }
        public TicketReceiptModel TicketReceipt { get; private set; }

    }
}
