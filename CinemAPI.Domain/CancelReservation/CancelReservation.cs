using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Domain.CancelReservation
{
    public class CancelReservation:ICancelReservation
    {
        private IReservationRepository reservationRepository;

        public CancelReservation(IReservationRepository reservationRepository)
        {
            this.reservationRepository = reservationRepository;
        }

        public CancelReservationSummary Cancel(long reservationId)
        {
            if (!reservationRepository.ReservationExists(reservationId))
            {
                return new CancelReservationSummary(false, "No such reservation exists!", System.Net.HttpStatusCode.NotFound);
            }
            else if (!reservationRepository.ReservationIsActive(reservationId))
            {
                return new CancelReservationSummary
                    (false, "Reservation is no longer active, and so cannot be cancelled!", System.Net.HttpStatusCode.BadRequest);
            }
            reservationRepository.CancelReservation(reservationId);
            return new CancelReservationSummary(true, "Success.", System.Net.HttpStatusCode.OK);
        }
    }
}
