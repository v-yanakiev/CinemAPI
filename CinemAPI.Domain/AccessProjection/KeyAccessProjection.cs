using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Projection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CinemAPI.Domain.AccessProjection
{
    public class KeyAccessProjection : IAccessProjection<int>
    {
        private readonly IProjectionRepository projectionsRepo;

        public KeyAccessProjection(IProjectionRepository projectionsRepo)
        {
            this.projectionsRepo = projectionsRepo;
        }

        public AccessProjectionSummary<int> Access(IProjection IProjection)
        {
            long id = IProjection.Id;
            var projection=(Projection)projectionsRepo.Get(id);
            if (projection == null)
            {
                return new AccessProjectionSummary<int>(false,false, "Projection not found!");
            }else if (projection.StartDate <= DateTime.Now)
            {
                return new AccessProjectionSummary<int>(true, false, "Cannot see the available seats of a started or finished projection!");

            }
            AccessProjectionSummary<int> accessProjectionSummary = new AccessProjectionSummary<int>(true, true, "Success");
            accessProjectionSummary.Data = projection.SeatCountAvailableAfterSubtracting;
            return accessProjectionSummary;
        }
    }
}
