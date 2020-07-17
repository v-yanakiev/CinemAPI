using CinemAPI.Data.EF;
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

namespace CinemAPI.Data.Implementation
{
    public class TicketRepository : ITicketRepository
    {
        private IProjectionRepository projectionRepository;
        private IReservationRepository reservationRepository;
        private CinemaDbContext dbContext;

        public TicketRepository(IProjectionRepository projectionRepository, IReservationRepository reservationRepository,
            CinemaDbContext cinemaDbContext)
        {
            this.projectionRepository = projectionRepository;
            this.reservationRepository = reservationRepository;
            this.dbContext = cinemaDbContext;
        }

        public ITicket BuyTicket(TicketBuyingModel ticketBuyingModel)
        {
            var ticket = new Ticket();
            ticket.ColumnNumber = ticketBuyingModel.ColumnNumber;
            ticket.RowNumber = ticketBuyingModel.RowNumber;
            ticket.ProjectionId = ticketBuyingModel.ProjectionId;
            this.dbContext.Tickets.Add(ticket);
            dbContext.SaveChanges();
            dbContext.Entry(ticket).Reference(a => a.Projection).Load();
            dbContext.Entry(ticket.Projection).Reference(a => a.Movie).Load();
            dbContext.Entry(ticket.Projection).Reference(a => a.Room).Load();
            dbContext.Entry(ticket.Projection.Room).Reference(a => a.Cinema).Load();

            return ticket;
        }

        public ITicket BuyTicket(long reservationId)
        {
            Reservation reservation = reservationRepository.GetReservation(reservationId);
            int rowNumber = reservation.RowNumber;
            int colNumber = reservation.ColumnNumber;
            Ticket ticket = new Ticket()
            {
                ProjectionId = reservation.ProjectionId,
                RowNumber = rowNumber,
                ColumnNumber = colNumber
            };
            this.dbContext.Tickets.Add(ticket);
            reservation.Deactivated = true;
            dbContext.SaveChanges();
            dbContext.Entry(ticket).Reference(a => a.Projection).Load();
            dbContext.Entry(ticket.Projection).Reference(a => a.Movie).Load();
            dbContext.Entry(ticket.Projection).Reference(a => a.Room).Load();
            dbContext.Entry(ticket.Projection.Room).Reference(a => a.Cinema).Load();

            return ticket;
        }

        public bool ProjectionHasStarted(long projectionId)
        {
            Projection projection = (Projection)projectionRepository.Get(projectionId);
            return projectionRepository.ProjectionHasStarted(projection);
        }

        public bool ReservationExists(long reservationId)
        {
            return(reservationRepository.GetReservation(reservationId)!=null);
        }

        public bool ReservationIsActive(long reservationId)
        {
            return this.reservationRepository.ReservationIsActive(reservationId);
        }

        public bool SeatIsOccupied(int RowNumber, int ColumnNumber, Projection projection)
        {
            return (dbContext.Tickets.Any(a => (a.ProjectionId == projection.Id) && a.RowNumber == RowNumber && a.ColumnNumber == ColumnNumber)
                ||reservationRepository.SeatIsReserved(RowNumber,ColumnNumber,projection));
        }
        public bool SeatIsOccupied(int RowNumber, int ColumnNumber, long projectionId)
        {
            Projection projection =(Projection) projectionRepository.Get(projectionId);
            return (dbContext.Tickets.Any(a => (a.ProjectionId == projection.Id) && a.RowNumber == RowNumber && a.ColumnNumber == ColumnNumber)
                || reservationRepository.SeatIsReserved(RowNumber, ColumnNumber, projection));
        }
        public bool SeatExists(int row, int col, long projectionId)
        {
            Projection projection = (Projection)projectionRepository.Get(projectionId);
            return (projection.Room.Rows >= row) && (projection.Room.SeatsPerRow >= col) && (row >= 1) && (col >= 1);
        }

    }
}
