using IdentityTutorial.Models.ViewModels;

namespace IdentityTutorial.Services
{
    public interface IGameService
    {
        public void TestMe();
        public int CreateNewGame(string userId);
        public int CreateNewGame(string userId, string opponentId);
        public GameVM GetGameVM(int gameId, string userId);
        public string GenerateGameTerrain();
        public bool DeleteGame(int id);
    }
}
