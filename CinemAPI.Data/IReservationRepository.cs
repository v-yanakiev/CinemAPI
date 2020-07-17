using CinemAPI.Models;
using CinemAPI.Models.Contracts;
using CinemAPI.Models.Contracts.Projection;
using InputModels.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Data
{
    public interface IReservationRepository
    {
        IReservation CreateNewReservation(ReservationCreationModel reservationCreationModel);
        bool SeatIsReserved(int row, int col,Projection projection);
        bool SeatExists(int row, int col, Projection projection);
        bool SeatsCanNoLongerBeReserved(Projection projection);
        IProjection GetProjection(long projectionId);
        bool ReservationExists(long reservationId);
        void CancelReservation(long reservationId);
        bool ReservationIsActive(long reservationId);
        Reservation GetReservation(long reservationId);
    }
}
