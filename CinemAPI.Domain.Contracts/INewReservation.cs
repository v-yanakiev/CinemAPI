using CinemAPI.Domain.Contracts.Models;
using InputModels.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Domain.Contracts
{
    public interface INewReservation
    {
        NewReservationSummary Create(ReservationCreationModel reservationCreationModel);
    }
}
