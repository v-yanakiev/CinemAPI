using System.ComponentModel.DataAnnotations;

namespace InputModels.Movie
{
    public class MovieCreationModel
    {
        [Required]
        public string Name { get; set; }
        [Required]

        public short DurationMinutes { get; set; }
    }
}