using System.ComponentModel.DataAnnotations;

namespace IdentityTutorial.Models
{
    public class Player
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } //user Guid
        [Required]
        public string PlayerName { get; set; } //like user name, but that was taken by id.core lol

        public DateTime LastActiveTime { get; set; } //time to see when the user was last active for match making

    }
}
