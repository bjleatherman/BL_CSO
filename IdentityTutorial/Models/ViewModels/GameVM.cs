using IdentityTutorial.Utilities;
using System.ComponentModel.DataAnnotations;

namespace IdentityTutorial.Models.ViewModels
{
    public class GameVM
    {
        //For Creating Gameboard
        [Key]
        public int GameId { get; set; }
        [Display(Name = "Game started")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Last Move Made")]
        public DateTime LastMoveDate { get; set; }
        public int MapLength { get; set; }
        public int MapWidth { get; set; }
        public string IslandCoordinates { get; set; }
        [Display(Name = "Max Health")]
        public int MaxHealth { get; set; }
        [Display(Name = "Full Mines")]
        public int MaxMines { get; set; }
        [Display(Name = "Full Drones")]
        public int MaxDrones { get; set; }
        [Display(Name = "Full Sneak")]
        public int MaxSneak { get; set; }
        [Display(Name = "Full Torpedo")]
        public int MaxTorpedo { get; set; }
        [Display(Name = "Full Sonar")]
        public int MaxSonar { get; set; }
        [Display(Name = "User Name")]

        //For Creating Player
        public string PlayerName { get; set; }
        [Display(Name = "Your Turn")]
        public bool IsTurn { get; set; }
        public string TotalDirectionHistory { get; set; }
        public string CurrentDirectionHistory { get; set; }
        public string TotalCoordinateHistory { get; set; }
        public string CurrentCoordinateHistory { get; set; }
        public int HealthValue { get; set; }
        public int MinesValue { get; set; }
        public int DronesValue { get; set; }
        public int SneakValue { get; set; }
        public int TorpedoValue { get; set; }
        public int SonarValue { get; set; }
        public string SystemsAttack { get; set; }
        public string SystemsDetect { get; set; }
        public string SystemsEvade { get; set; }
        public string SystemsReactor { get; set; }

        //For Creating Enemy
        [Display(Name = "Enemy Name")]
        public string EnemyName { get; set; }
        public string EnemyDirectionHisory { get; set; }
        public string EnemyCurrentDirectionHistory { get; set; }
        [Display(Name = "Enemy Health")]
        public int EnemyHealth { get; set; }

        //Static Strings
        //Power Names
        public string Health { get; set; }
        public string Mines { get; set; }
        public string Drones { get; set; }
        public string Sneak { get; set; }
        public string Torpedo { get; set; }
        public string Sonar { get; set; }
        public string AllCharged { get; set; }
        public string West { get; set; }
        public string North { get; set; }
        public string South { get; set; }
        public string East { get; set; }
        public string Surface { get; set; }
        public string FirstMove { get; set; }
    }
}
