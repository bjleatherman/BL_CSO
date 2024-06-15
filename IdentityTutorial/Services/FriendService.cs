using IdentityTutorial.Areas.Identity.Data;
using IdentityTutorial.Models;
using IdentityTutorial.Models.ViewModels;
using IdentityTutorial.Utilities;

namespace IdentityTutorial.Services
{
    public class FriendService : IFriendService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPlayerService _playerService;
        public FriendService(ApplicationDbContext context, IPlayerService playerService)
        {
            _context = context;
            _playerService = playerService;
        }

        public bool AcceptFriendRequest(string userId, string friendName)
        {
            string friendId = _playerService.GetUserIdFromPlayerName(friendName);

            // Set Status code for both users PlayerFriend record to 2
            PlayerFriend userPF = _context.PlayerFriends.Where(x => x.PlayerId == userId && x.FriendId == friendId).FirstOrDefault() ??
                throw new Exception("Could not find PlayerFriend record in table");
            PlayerFriend friendPF = _context.PlayerFriends.Where(x => x.PlayerId == friendId && x.FriendId == userId).FirstOrDefault() ??
                throw new Exception("Could not find PlayerFriend record in table");

            userPF.Status = (int)Helper.FRIEND_STATUS.Confirmed;
            friendPF.Status = (int)Helper.FRIEND_STATUS.Confirmed;

            try
            {
                _context.Update(userPF);
                _context.Update(friendPF);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AddFriend(string userId, string friendName)
        {
            string friendId = _playerService.GetUserIdFromPlayerName(friendName);

            PlayerFriend requestorPF = new PlayerFriend()
            {
                PlayerId = userId,
                FriendId = friendId,
                Status = (int)Helper.FRIEND_STATUS.Pending
            };

            PlayerFriend inviteePF = new PlayerFriend()
            {
                PlayerId = friendId,
                FriendId = userId,
                Status = (int)Helper.FRIEND_STATUS.Recieved
            };

            try
            {
                _context.Add(requestorPF);
                _context.Add(inviteePF);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<PlayerFriendVM> GetFriendList(string userId)
        {
            List<PlayerFriend> playerFriendList = _context.PlayerFriends.Where(x => x.PlayerId == userId).ToList();
            var playerFriendVM = playerFriendList.Select(p => new PlayerFriendVM
            {
                FriendId = p.Id,
                FriendName = _playerService.GetPlayerName(p.FriendId),
                Status = ((Helper.FRIEND_STATUS)p.Status).ToString(),
                StatusCode = p.Status,
            }).OrderByDescending(p => p.Status).ToList();

            return playerFriendVM;
        }

        public bool IsValidFriend(string userId, string friendName)
        {
            // If the player does not exist, return false 
            if(!_playerService.DoesPlayerNameExist(friendName)) { return false; }

            // If the friendId matches the userId, return false
            string friendId = _playerService.GetUserIdFromPlayerName(friendName);
            if (friendId == userId) { return false; }

            // If the friendId is already in the users friends list, return false
            if (_context.PlayerFriends.Any(x => x.PlayerId == userId && x.FriendId == friendId)) { return false; }

            return true;

        }

        public bool DeleteFriend(string userId, string friendName)
        {
            string friendId = _playerService.GetUserIdFromPlayerName(friendName);

            // Set Status code for both users PlayerFriend record to 2
            PlayerFriend userPF = _context.PlayerFriends.Where(x => x.PlayerId == userId && x.FriendId == friendId).FirstOrDefault() ??
                throw new Exception("Could not find PlayerFriend record in table");
            PlayerFriend friendPF = _context.PlayerFriends.Where(x => x.PlayerId == friendId && x.FriendId == userId).FirstOrDefault() ??
                throw new Exception("Could not find PlayerFriend record in table");

            try
            {
                _context.Remove(userPF);
                _context.Remove(friendPF);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
