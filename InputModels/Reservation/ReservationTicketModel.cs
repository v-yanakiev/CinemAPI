using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputModels.Reservation
{
    public class ReservationTicketModel
    {
        public ReservationTicketModel(long reservationKey, DateTime projectionStartDate, string movieName, 
            string cinemaName, int roomNumber, int row, int column)
        {
            ReservationKey = reservationKey;
            ProjectionStartDate = projectionStartDate;
            MovieName = movieName;
            CinemaName = cinemaName;
            RoomNumber = roomNumber;
            Row = row;
            Column = column;
        }

        public long ReservationKey { get; set; }

        public DateTime ProjectionStartDate { get; set; }
        public string MovieName { get; set; }
        public string CinemaName { get; set; }
        public int RoomNumber { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }
}
