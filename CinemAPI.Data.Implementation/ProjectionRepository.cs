using CinemAPI.Data.EF;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Projection;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CinemAPI.Data.Implementation
{
    public class ProjectionRepository : IProjectionRepository
    {
        private readonly CinemaDbContext db;

        public ProjectionRepository(CinemaDbContext db)
        {
            this.db = db;
        }

        public IProjection Get(int movieId, int roomId, DateTime startDate)
        {
            return db.Projections.FirstOrDefault(x => x.MovieId == movieId &&
                                                      x.RoomId == roomId &&
                                                      x.StartDate == startDate);
        }
        public IProjection Get(long projectionId)
        {
            return db.Projections.Include(a=>a.Room.Cinema).Include(a=>a.Movie).Include(a=>a.Reservations).Include(a=>a.Tickets).FirstOrDefault(x => x.Id == projectionId);
        }
        public IEnumerable<IProjection> GetActiveProjections(int roomId)
        {
            DateTime now = DateTime.UtcNow;

            return db.Projections.Where(x => x.RoomId == roomId &&
                                             x.StartDate > now);
        }

        public void Insert(IProjectionCreation proj)
        {
            List<Projection> projections = db.Projections.ToList();
            Projection newProj = new Projection(proj.MovieId, proj.RoomId, proj.StartDate,proj.AvailableSeatsCount);

            db.Projections.Add(newProj);
            db.SaveChanges();
        }

        public bool ProjectionHasStarted(IProjection projection)
        {
            return (projection.StartDate <= DateTime.Now);
        }
        public bool ProjectionHasStarted(long projectionId)
        {
            var projection = this.Get(projectionId);
            return (projection.StartDate <= DateTime.Now);
        }
    }
}