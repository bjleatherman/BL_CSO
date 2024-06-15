using System.ComponentModel.DataAnnotations;

namespace IdentityTutorial.Models.ViewModels
{
    public class PlayerFriendVM
    {
        [Key]
        [Required]
        public int FriendId { get; set; }

        [Required]
        [Display(Name = "Friend Name")]
        public string FriendName { get; set; }

        [Required]
        [Display(Name = "Status")]
        public string Status { get; set; }

        [Required]
        public int StatusCode { get; set; }

    }
}
