using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models;
using CinemAPI.Models.Contracts;
using InputModels.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewReservation
{
    public class NewReservationCreation : INewReservation
    {
        private IReservationRepository reservationRepository;
        private ITicketRepository ticketRepository;

        public NewReservationCreation(IReservationRepository reservationRepository, ITicketRepository ticketRepository)
        {
            this.reservationRepository = reservationRepository;
            this.ticketRepository = ticketRepository;
        }

        public NewReservationSummary Create(ReservationCreationModel reservationCreationModel)
        {
            Projection projection = (Projection)reservationRepository.GetProjection(reservationCreationModel.ProjectionId);

            if (projection == null)
            {
                return new NewReservationSummary(false, System.Net.HttpStatusCode.NotFound, "Projection not found!", null);
            }
            else if (reservationRepository.SeatsCanNoLongerBeReserved(projection))
            {
                return new NewReservationSummary(false, System.Net.HttpStatusCode.BadRequest, "You can no longer reserve seats for this projection!", null);
            }else if (!reservationRepository.SeatExists(reservationCreationModel.RowNumber, reservationCreationModel.ColumnNumber,projection))
            {
                return new NewReservationSummary(false, System.Net.HttpStatusCode.BadRequest, "Invalid seat or column number!", null);
            }
            else if(ticketRepository.SeatIsOccupied(reservationCreationModel.RowNumber, reservationCreationModel.ColumnNumber, projection)){
                return new NewReservationSummary(false, System.Net.HttpStatusCode.BadRequest, "That seat is already taken!", null);
            }
            IReservation reservation= reservationRepository.CreateNewReservation(reservationCreationModel);
            ReservationTicketModel reservationTicketModel =
                new ReservationTicketModel
                (reservation.Id, projection.StartDate, projection.Movie.Name, projection.Room.Cinema.Name, 
                projection.Room.Number, reservation.RowNumber, reservation.ColumnNumber);
            return new NewReservationSummary(true, System.Net.HttpStatusCode.OK, "Created Successfully!", reservationTicketModel);
        }
    }
}
