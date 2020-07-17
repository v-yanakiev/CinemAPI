using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputModels.Projection
{
    public class GetAvailableSeatsModel
    {
        [Required]
        public long ProjectionId { get; set; }
    }
}
