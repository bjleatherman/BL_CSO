using IdentityTutorial.Utilities;
using System.ComponentModel.DataAnnotations;

namespace IdentityTutorial.Models.ViewModels
{
    public class TurnVM
    {
        [Required]
        public int GameId { get; set; }
        [Required] 
        public string UserId { get; set; }
        [Required]
        public string Direction { get; set; }
        [Required]
        public string Systems { get; set; }
        [Required]
        public string Power { get; set; }
        public int? FirstMoveLocation { get; set; }
        public string? ActivatedPower { get; set; }
        public int? ActivedPowerValue { get; set; }
        public string? Message { get; set; }
        public int? StartingMove { get; set; }
        public int? SonarTrueType { get; set; }
        public int? SonarTrueValue { get; set; }
        public int? SonarFalseType { get; set; }
        public int? SonarFalseValue { get; set; }


    }
}
