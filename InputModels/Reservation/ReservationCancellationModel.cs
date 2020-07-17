using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputModels.Reservation
{
    public class ReservationCancellationModel
    {
        [Required]

        public long ReservationId { get; set; }
    }
}
