using CinemAPI.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Models
{
    public class Reservation:IReservation
    {
        public long Id { get; set; }
        public long ProjectionId { get; set; }
        public virtual Projection Projection { get; set; }
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }
        public virtual bool IsActive { get {
                if ((this.Projection.StartDate - DateTime.Now).TotalMinutes <= 10)
                {
                    return false;
                }
                else
                {
                    return (!Deactivated);
                }
            } }
        public bool Deactivated { get; set; }
    }
}
