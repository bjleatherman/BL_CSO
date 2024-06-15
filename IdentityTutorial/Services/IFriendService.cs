using IdentityTutorial.Models.ViewModels;

namespace IdentityTutorial.Services
{
    public interface IFriendService
    {
        public IEnumerable<PlayerFriendVM> GetFriendList(string userId);
        public bool IsValidFriend (string userId, string friendName);
        public bool AddFriend(string userId, string friendName);
        public bool DeleteFriend(string userId, string friendName);
        public bool AcceptFriendRequest(string userId, string friendName);
    }
}
