using CinemAPI.Domain.Contracts.Models;
using InputModels.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Domain.Contracts
{
    public interface IBuyTicket
    {
        BuyTicketSummary BuyWithoutReservation(TicketBuyingModel ticketBuyingModel);
        BuyTicketSummary BuyWithReservation(long reservationId);
    }
}
