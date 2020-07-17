using CinemAPI.Data.EF;
using CinemAPI.Models;
using CinemAPI.Models.Contracts;
using CinemAPI.Models.Contracts.Projection;
using InputModels.Reservation;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Data.Implementation
{
    public class ReservationRepository: IReservationRepository
    {
        private readonly CinemaDbContext db;
        private readonly IProjectionRepository projectionRepository;

        public ReservationRepository(CinemaDbContext db, IProjectionRepository projectionRepository)
        {
            this.db = db;
            this.projectionRepository = projectionRepository;
        }
        
        public IReservation CreateNewReservation(ReservationCreationModel reservationCreationModel)
        {
            Projection projection = (Projection)this.GetProjection(reservationCreationModel.ProjectionId);
            
            Reservation reservation =
                new Reservation()
                {
                    ProjectionId = projection.Id,
                    ColumnNumber = reservationCreationModel.ColumnNumber,
                    RowNumber=reservationCreationModel.RowNumber,
                    Deactivated=false
                };
            db.Reservations.Add(reservation);
            db.SaveChanges();
            return reservation;
        }
        public Reservation GetReservation(long reservationId)
        {
            return this.db.Reservations.Include(a=>a.Projection).FirstOrDefault(a=>a.Id==reservationId);
        }
        public bool SeatExists(int row, int col, Projection projection)
        {
            return (projection.Room.Rows >= row) && (projection.Room.SeatsPerRow >= col)&&(row>=1)&&(col>=1);
        }

        public bool SeatIsReserved(int row, int col, Projection projection)
        {
            db.Entry(projection).Collection(a=>a.Reservations).Load();
            return projection.Reservations.Any(a => (a.ColumnNumber == col && a.RowNumber == row&&a.IsActive));
        }

        public bool SeatsCanNoLongerBeReserved(Projection projection)
        {
            return ((projection.StartDate - DateTime.Now).TotalMinutes <= 10);
        }
        public IProjection GetProjection(long projectionId)
        {
            return projectionRepository.Get(projectionId);
        }

        public bool ReservationExists(long reservationId)
        {
            return db.Reservations.Any(a => a.Id == reservationId);
        }

        public void CancelReservation(long reservationId)
        {
            db.Reservations.Find(reservationId).Deactivated = true;
            db.SaveChanges();
        }
        public bool ReservationIsActive(long reservationId)
        {
            var reservation = this.GetReservation(reservationId);
            if (reservation == null)
            {
                return false;
            }
            return reservation.IsActive;
        }
    }
}
