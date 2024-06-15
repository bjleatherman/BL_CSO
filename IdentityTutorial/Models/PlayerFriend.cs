using System.ComponentModel.DataAnnotations;

namespace IdentityTutorial.Models
{
    public class PlayerFriend
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string PlayerId { get; set; }

        [Required]
        public string FriendId { get; set; }

        [Required]
        public int Status { get; set; }
    }
}
