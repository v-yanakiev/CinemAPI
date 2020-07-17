using System;
using System.ComponentModel.DataAnnotations;

namespace InputModels.Projection
{
    public class ProjectionCreationModel
    {
        [Required]
        public int RoomId { get; set; }
        [Required]
        public int MovieId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string StartDate { get; set; }
        [Required]
        public int AvailableSeatsCount { get; set; }
    }
}