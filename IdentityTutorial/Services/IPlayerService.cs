using IdentityTutorial.Models;
using Microsoft.AspNetCore.Mvc;

namespace IdentityTutorial.Services
{
    public interface IPlayerService
    {
        //CW "PlayerService.TestService" /n string passed in
        public void TestService(string userId);
        public void CreatePlayer(string userId, string playerName);
        public string GetPlayerName(string userId);
        public string GetRandomPlayerId();
        public PlayerGameSave CreatePlayerGameSave(int gameId, string playerId, bool isInitiating);
        public Player GetPlayer(string userId);
        public bool DoesPlayerNameExist(string playerName);
        public string GetUserIdFromPlayerName(string playerName);
    }
}
