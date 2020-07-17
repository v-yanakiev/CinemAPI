using System.ComponentModel.DataAnnotations;

namespace InputModels.Room
{
    public class RoomCreationModel
    {
        [Required]
        public int Number { get; set; }

        [Required]
        public short SeatsPerRow { get; set; }

        [Required]
        public short Rows { get; set; }

        [Required]
        public int CinemaId { get; set; }
    }
}