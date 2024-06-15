using IdentityTutorial.Models;
using IdentityTutorial.Models.ViewModels;

namespace IdentityTutorial.Services
{
    public interface IUserGameService
    {
        public List<UserGameVM> GetUserGameVM(string userId);
        public UserGame CreateUserGame(int gameId, string player1Id, string player2Id);
    }
}
