using IdentityTutorial.Areas.Identity.Data;
using IdentityTutorial.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace IdentityTutorial.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly ApplicationDbContext _context;

        public PlayerService(ApplicationDbContext context)
        {
            _context = context;
        }
        public void CreatePlayer(string userId, string playerName)
        {
            // TODO: make this async and for the love of god add some model validation

            var _player = _context.Players;

            Player player = new Player()
            {
                UserId = userId,
                PlayerName = playerName,
                LastActiveTime = DateTime.Now
            };

            _context.Add(player); //async here
            _context.SaveChanges(); //async here too 

        }

        public string GetPlayerName(string userId)
        {
            //TODO: add possibility to check for nulls, and possibly do this async
            return _context.Players?.FirstOrDefault(x => x.UserId == userId)?.PlayerName ??
                throw new Exception("userId did not return a value");
        }

        public string GetRandomPlayerId()
        {
            Random random = new Random();
            var _users = _context.Users;
            string player = "";
            int toSkip = random.Next(1, _users.Count());

            player = _users.Skip(toSkip).Take(1).First().Id.ToString();

            return player;
        }

        public void TestService(string userId)
        {
            Console.WriteLine("Called PlayerService.TestService");
            Console.WriteLine(userId);
        }

        public PlayerGameSave CreatePlayerGameSave(int gameId, string playerId, bool isInitiating)
        {
            PlayerGameSave save = new PlayerGameSave()
            {
                GameId = gameId,
                PlayerId = playerId,
                IsChallenger = isInitiating,
                IsTurn = isInitiating,
                TotalDirectionHistory = "",
                CurrentDirectionHistory = "",
                TotalCoordinateHistory = "",
                CurrentCoordinateHistory = "",
                Health = 4,
                Mines = 0,
                Drones = 0,
                Sneak = 0,
                Torpedo = 0,
                Sonar = 0,
                SystemsAttack = "true,true,true,true,true,true",
                SystemsDetect = "true,true,true,true,true,true",
                SystemsEvade = "true,true,true,true,true,true",
                SystemsReactor = "true,true,true,true,true,true"
            };

            return save;
        }

        public Player GetPlayer(string userId)
        {
            Player _player = _context.Players?.FirstOrDefault(x => x.UserId == userId) ??
                throw new Exception("Player record not found");

            return _player;
        }

        public static void UpdatePlayerLastDateTime(string userId, ApplicationDbContext _context)
        {
            Player _player = _context.Players?.FirstOrDefault(x => x.UserId == userId) ??
                throw new Exception("Player record not found");

            _player.LastActiveTime = DateTime.Now;
            _context.Update(_player);
            _context.SaveChanges();
        }

        public bool DoesPlayerNameExist(string playerName)
        {
            return _context.Players?.Any(x => x.PlayerName == playerName) ?? false;
        }

        public string GetUserIdFromPlayerName(string playerName)
        {
            return _context.Players?.FirstOrDefault(x => x.PlayerName == playerName)?.UserId ??
                throw new Exception("playerName did not return a value");
        }

        // TODO: create static method that takes in the userId and updates
        // their last active time in the database to DateTime.Now
    }
}
