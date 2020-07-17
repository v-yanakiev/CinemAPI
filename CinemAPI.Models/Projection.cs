using CinemAPI.Models.Contracts.Projection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CinemAPI.Models
{
    public class Projection : IProjection, IProjectionCreation,IProjectionAccessParameters
    {
        public Projection()
        {
        }

        public Projection(int movieId, int roomId, DateTime startdate,int availableSeatsCount)
        {
            this.MovieId = movieId;
            this.RoomId = roomId;
            this.StartDate = startdate;
            this.AvailableSeatsCount = availableSeatsCount;
        }

        public long Id { get; set; }

        public int RoomId { get; set; }

        public virtual Room Room { get; set; }

        public int MovieId { get; set; }
        
        public virtual Movie Movie { get; set; }

        public DateTime StartDate { get; set; }

        [Range(0, double.PositiveInfinity)]
        public int AvailableSeatsCount { get; set; }

        public virtual int SeatCountAvailableAfterSubtracting { 
            get => (this.AvailableSeatsCount - (this.Reservations.Count(a => a.IsActive) + this.Tickets.Count()));
        }
        public long ReservationId { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}