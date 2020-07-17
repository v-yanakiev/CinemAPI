using CinemAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Data.EF.ModelConfigurations
{
    public class TicketModelConfiguration : IModelConfiguration
    {
        public void Configure(DbModelBuilder modelBuilder)
        {
            EntityTypeConfiguration<Ticket> ticketModel = modelBuilder.Entity<Ticket>();
            ticketModel.HasKey(a => a.Id);
            ticketModel.Property(a => a.ProjectionId).IsRequired();
            ticketModel.Property(a => a.RowNumber).IsRequired();
            ticketModel.Property(a => a.ColumnNumber).IsRequired();
        }
    }
}
