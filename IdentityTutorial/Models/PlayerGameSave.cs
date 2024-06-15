using System.ComponentModel.DataAnnotations;

namespace IdentityTutorial.Models
{
    public class PlayerGameSave
    {

        // TODO:
        // add turns left property => add it to the GameVM
        // add last active property => add it to the GameVM

        [Required]
        public int Id { get; set; }

        [Required]
        public int GameId { get; set; }

        [Required]
        public string PlayerId { get; set; }

        [Required]
        public bool IsChallenger { get; set; }

        [Required]
        public bool IsTurn { get; set; }

        [Required]
        public string TotalDirectionHistory { get; set; }

        [Required]
        public string CurrentDirectionHistory { get; set; }

        [Required]
        public string TotalCoordinateHistory { get; set; }

        [Required]
        public string CurrentCoordinateHistory { get; set; }

        [Required]
        public int Health { get; set; }

        [Required]
        public int Mines { get; set; }

        [Required]
        public int Drones { get; set; }

        [Required]
        public int Sneak { get; set; }

        [Required]
        public int Torpedo { get; set; }

        [Required]
        public int Sonar { get; set; }

        [Required]
        public string SystemsAttack { get; set; }

        [Required]
        public string SystemsDetect { get; set; }

        [Required]
        public string SystemsEvade { get; set; }

        [Required]
        public string SystemsReactor { get; set; }
    }
}
