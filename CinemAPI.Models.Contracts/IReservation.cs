﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Models.Contracts
{
    public interface IReservation
    {
        long Id { get; set; }
        long ProjectionId { get; set; }
        int RowNumber { get; set; }
        int ColumnNumber { get; set; }
        bool Deactivated { get; set; }
    }
}
