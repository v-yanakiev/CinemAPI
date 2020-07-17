using CinemAPI.Models.Contracts.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Models
{
    public class Ticket:ITicket
    {
        public long Id { get; set; }
        public long ProjectionId { get; set; }
        public Projection Projection { get; set; }
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }

    }
}
