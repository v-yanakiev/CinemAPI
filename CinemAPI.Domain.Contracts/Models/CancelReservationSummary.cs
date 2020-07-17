using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Domain.Contracts.Models
{
    public class CancelReservationSummary
    {
        public CancelReservationSummary(bool success, string message, HttpStatusCode statusCode)
        {
            Success = success;
            Message = message;
            StatusCode = statusCode;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
