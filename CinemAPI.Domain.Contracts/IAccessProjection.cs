using CinemAPI.Domain.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemAPI.Models.Contracts.Projection;
namespace CinemAPI.Domain.Contracts
{
    public interface IAccessProjection<T>
    {
        AccessProjectionSummary<T> Access(IProjection projection);

    }
}
