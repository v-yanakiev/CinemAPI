using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models;
using InputModels.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Domain.BuyTicket
{
    public class BuyTicket : IBuyTicket
    {
        private ITicketRepository ticketRepository;
        private IProjectionRepository projectionRepository;

        public BuyTicket(ITicketRepository ticketRepository, IProjectionRepository projectionRepository)
        {
            this.ticketRepository = ticketRepository;
            this.projectionRepository = projectionRepository;
        }

        public BuyTicketSummary BuyWithoutReservation(TicketBuyingModel ticketBuyingModel)
        {
            if (projectionRepository.Get(ticketBuyingModel.ProjectionId) == null)
            {
                return new BuyTicketSummary(false, System.Net.HttpStatusCode.NotFound, "Projection not found!", null);
            }
            else if (!this.ticketRepository.SeatExists(ticketBuyingModel.RowNumber, ticketBuyingModel.ColumnNumber, ticketBuyingModel.ProjectionId))
            {
                return new BuyTicketSummary(false, System.Net.HttpStatusCode.BadRequest, "Invalid seat!", null);
            }
            else if (ticketRepository.SeatIsOccupied(ticketBuyingModel.RowNumber, ticketBuyingModel.ColumnNumber, ticketBuyingModel.ProjectionId)){
                return new BuyTicketSummary(false, System.Net.HttpStatusCode.BadRequest, "The seat you have chosen is occupied!", null);
            }else if (projectionRepository.ProjectionHasStarted(ticketBuyingModel.ProjectionId))
            {
                return new BuyTicketSummary(false, System.Net.HttpStatusCode.BadRequest, "You cannot buy a ticket for a started or finished projection!", null);
            }
            Ticket ticket = (Ticket)ticketRepository.BuyTicket(ticketBuyingModel);
            TicketReceiptModel ticketReceiptModel = new TicketReceiptModel()
            {
                TicketId = ticket.Id,
                CinemaName = ticket.Projection.Room.Cinema.Name,
                MovieName = ticket.Projection.Movie.Name,
                ColumnNumber = ticket.ColumnNumber,
                RowNumber = ticket.RowNumber,
                ProjectionStartDate = ticket.Projection.StartDate,
                RoomNumber = ticket.Projection.Room.Number
            };
            return new BuyTicketSummary(true, System.Net.HttpStatusCode.OK, "Ticket bought!", ticketReceiptModel);

        }

        public BuyTicketSummary BuyWithReservation(long reservationId)
        {
            if (!ticketRepository.ReservationExists(reservationId))
            {
                return new BuyTicketSummary(false, System.Net.HttpStatusCode.NotFound, "Reservation not found!", null);
            }
            else if (!ticketRepository.ReservationIsActive(reservationId))
            {
                return new BuyTicketSummary(false, System.Net.HttpStatusCode.BadRequest, "You can no longer use this reservation!", null);
            }
            Ticket ticket =(Ticket)ticketRepository.BuyTicket(reservationId);

            TicketReceiptModel ticketReceiptModel = new TicketReceiptModel()
            {
                TicketId = ticket.Id,
                CinemaName = ticket.Projection.Room.Cinema.Name,
                MovieName = ticket.Projection.Movie.Name,
                ColumnNumber = ticket.ColumnNumber,
                RowNumber = ticket.RowNumber,
                ProjectionStartDate = ticket.Projection.StartDate,
                RoomNumber = ticket.Projection.Room.Number
            };
            return new BuyTicketSummary(true, System.Net.HttpStatusCode.OK, "Ticket bought!", ticketReceiptModel);

        }
    }
}
