using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputModels.Ticket
{
    public class TicketReceiptModel
    {
        public long TicketId { get; set; }
        public DateTime ProjectionStartDate { get; set; }
        public string MovieName { get; set; }
        public string CinemaName { get; set; }
        public int RoomNumber { get; set; }
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }
    }
}
