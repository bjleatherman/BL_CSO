using System.ComponentModel.DataAnnotations;

namespace IdentityTutorial.Models
{
    public class Game
    {
        [Required]
        public int GameId { get; set; }

        [Required]
        [Display(Name = "Game started")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Last Move Made")]
        public DateTime LastMoveDate { get; set; }

        [Required]
        public int MapLength { get; set; }
        [Required]
        public int MapWidth { get; set; }

        [Required]
        public string IslandCoordinates { get; set; }

        [Required]
        public int MaxHealth { get; set; }

        [Required]
        public int MaxMines { get; set; }

        [Required]
        public int MaxDrones { get; set; }

        [Required]
        public int MaxSneak { get; set; }

        [Required]
        public int MaxTorpedo { get; set; }

        [Required]
        public int MaxSonar { get; set; }

    }
}
 