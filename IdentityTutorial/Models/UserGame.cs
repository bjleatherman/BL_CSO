using System.ComponentModel.DataAnnotations;

namespace IdentityTutorial.Models
{
    public class UserGame
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string User1Id { get; set; }
        [Required]
        public string User2Id { get; set; }
        [Required]
        public int GameId { get; set; }
    }
}
