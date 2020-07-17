using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Domain.Contracts.Models
{
    public class AccessProjectionSummary<T>
    {
        public AccessProjectionSummary(bool exists, bool hasStarted)
        {
            this.Exists = exists;
            this.ValidDateTime = hasStarted;
        }

        public AccessProjectionSummary(bool exists, bool hasStarted, string msg)
            : this(exists,hasStarted)
        {
            this.Message = msg;
        }

        public string Message { get; private set; }

        public bool Exists { get; private set; }
        public bool ValidDateTime { get; private set; }
        public T Data { get; set; }
    }
}
