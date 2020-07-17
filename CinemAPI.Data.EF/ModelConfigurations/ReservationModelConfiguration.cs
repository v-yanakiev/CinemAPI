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
    internal sealed class ReservationModelConfiguration : IModelConfiguration
    {
        public void Configure(DbModelBuilder modelBuilder)
        {
            EntityTypeConfiguration<Reservation> reservationModel = modelBuilder.Entity<Reservation>();
            reservationModel.HasKey(a => a.Id);
            reservationModel.Property(a => a.ProjectionId).IsRequired();
            reservationModel.Property(a => a.RowNumber).IsRequired();
            reservationModel.Property(a => a.ColumnNumber).IsRequired();
        }
    }
}
