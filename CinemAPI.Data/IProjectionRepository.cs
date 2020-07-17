using CinemAPI.Models.Contracts.Projection;
using System;
using System.Collections.Generic;

namespace CinemAPI.Data
{
    public interface IProjectionRepository
    {
        IProjection Get(int movieId, int roomId, DateTime startDate);
        IProjection Get(long projectionId);
        void Insert(IProjectionCreation projection);

        IEnumerable<IProjection> GetActiveProjections(int roomId);
        bool ProjectionHasStarted(IProjection projection);
        bool ProjectionHasStarted(long projectionId);
    }
}