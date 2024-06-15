using System.ComponentModel.DataAnnotations;

namespace IdentityTutorial.Models.ViewModels
{
    public class UserGameVM
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Player")]
        public string Player1 { get; set; }

        [Display(Name = "Opponent")]
        public string Player2 { get; set; }

        [Display(Name = "Game Started On")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Game Id")]
        public int GameId { get; set; }
    }
}
