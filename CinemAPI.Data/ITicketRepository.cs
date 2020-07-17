using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Contracts.Ticket;
using InputModels.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Data
{
    public interface ITicketRepository
    {
        bool SeatIsOccupied(int RowNumber, int ColumnNumber,Projection projection);
        bool ProjectionHasStarted(long projectionId);
        ITicket BuyTicket(TicketBuyingModel ticketBuyingModel);
        ITicket BuyTicket(long reservationId);
        bool ReservationIsActive(long reservationId);
        bool SeatIsOccupied(int RowNumber, int ColumnNumber, long projectionId);
        bool ReservationExists(long reservationId);
        bool SeatExists(int row, int col, long projectionId);
    }
}
