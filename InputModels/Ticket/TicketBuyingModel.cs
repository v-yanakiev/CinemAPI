using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputModels.Ticket
{
    public class TicketBuyingModel
    {
        [Required]
        public long ProjectionId { get; set; }
        [Required]

        public int RowNumber { get; set; }
        [Required]
        public int ColumnNumber { get; set; }

    }
}
