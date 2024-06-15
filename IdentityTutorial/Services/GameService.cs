using IdentityTutorial.Areas.Identity.Data;
using IdentityTutorial.Models;
using IdentityTutorial.Models.ViewModels;
using IdentityTutorial.Utilities;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace IdentityTutorial.Services
{
    public class GameService : IGameService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPlayerService _playerService;
        private readonly IUserGameService _userGameService;

        public GameService(ApplicationDbContext context, IPlayerService playerService, IUserGameService userGameService)
        {
            _context = context;
            _playerService = playerService;
            _userGameService = userGameService;
        }
        public void TestMe()
        {
            Console.WriteLine("Called TestMe in GameService");
        }

        public int CreateNewGame(string userId)
        {
            //TODO: Make Async and comment

            var _users = _context.Users;
            var _games = _context.Games;
            string opponentId = _playerService.GetRandomPlayerId();
            int newGameId = 0;
            PlayerGameSave player1GameSave = new PlayerGameSave();
            PlayerGameSave player2GameSave = new PlayerGameSave();
            UserGame userGame = new UserGame();

            while (userId == opponentId)
            {
                opponentId = _playerService.GetRandomPlayerId();
            }

            //Game
            Game game = new Game()
            {
                StartDate = DateTime.Now,
                LastMoveDate = DateTime.Now,
                MapLength = 15,
                MapWidth = 15,
                IslandCoordinates = GenerateGameTerrain(),
                //IslandCoordinates = "32,37,42,107,112,117,182,187,192", //These will be generated at some point in time 
                MaxHealth = 4,
                MaxMines = 3,
                MaxDrones = 4,
                MaxSneak = 6,
                MaxTorpedo = 3,
                MaxSonar = 3
            };

            //TODO: Rollback changes if future pushes fail
            _context.Add(game);
            _context.SaveChanges();

            newGameId = _games.Max(x => x.GameId);

            //PlayerGameSave for Player1
            player1GameSave = _playerService.CreatePlayerGameSave(newGameId, userId, true);

            //PlayerGameSave for Player2
            player2GameSave = _playerService.CreatePlayerGameSave(newGameId, opponentId, false);

            //UserGame
            userGame = _userGameService.CreateUserGame(newGameId, userId, opponentId);

            //This part should be async I thync lol
            _context.Add(player1GameSave);
            _context.Add(player2GameSave);
            _context.Add(userGame);
            _context.SaveChanges();

            return newGameId;
        }
        public int CreateNewGame(string userId, string opponentId)
        {
            //TODO: Make Async and comment

            var _users = _context.Users;
            var _games = _context.Games;
            int newGameId = 0;
            PlayerGameSave player1GameSave = new PlayerGameSave();
            PlayerGameSave player2GameSave = new PlayerGameSave();
            UserGame userGame = new UserGame();

            //Game
            Game game = new Game()
            {
                StartDate = DateTime.Now,
                LastMoveDate = DateTime.Now,
                MapLength = 15,
                MapWidth = 15,
                IslandCoordinates = GenerateGameTerrain(),
                //IslandCoordinates = "32,37,42,107,112,117,182,187,192", //These will be generated at some point in time 
                MaxHealth = 4,
                MaxMines = 3,
                MaxDrones = 4,
                MaxSneak = 6,
                MaxTorpedo = 3,
                MaxSonar = 3
            };

            //TODO: Rollback changes if future pushes fail
            _context.Add(game);
            _context.SaveChanges();

            newGameId = _games.Max(x => x.GameId);

            //PlayerGameSave for Player1
            player1GameSave = _playerService.CreatePlayerGameSave(newGameId, userId, true);

            //PlayerGameSave for Player2
            player2GameSave = _playerService.CreatePlayerGameSave(newGameId, opponentId, false);

            //UserGame
            userGame = _userGameService.CreateUserGame(newGameId, userId, opponentId);

            //This part should be async I thync lol
            _context.Add(player1GameSave);
            _context.Add(player2GameSave);
            _context.Add(userGame);
            _context.SaveChanges();

            return newGameId;
        }
        public GameVM GetGameVM(int gameId, string userId)
        {

            /*
             * TODO: add comments and make async
             * **/

            var _games = _context.Games.ToList();
            var _playerGameSaves = _context.PlayerGameSaves.ToList();

            var game = _games.Where(x => x.GameId == gameId).FirstOrDefault();  

            var userGameSave = _playerGameSaves.Where(
                x => x.GameId == gameId &&
                x.PlayerId == userId).FirstOrDefault();


            var opponentGameSave = _playerGameSaves.Where(
                x => x.GameId == gameId &&
                x.PlayerId != userId).FirstOrDefault();

            GameVM gameVm = new GameVM()
            {
                GameId = game.GameId,
                StartDate = game.StartDate,
                LastMoveDate = game.LastMoveDate,
                MapLength = game.MapLength,
                MapWidth = game.MapWidth,
                IslandCoordinates = game.IslandCoordinates, //convert to array here? research if possible
                MaxHealth = game.MaxHealth,
                MaxMines = game.MaxMines,
                MaxDrones = game.MaxDrones,
                MaxSneak = game.MaxSneak,
                MaxTorpedo = game.MaxTorpedo,
                MaxSonar = game.MaxSonar,
                PlayerName = _playerService.GetPlayerName(userGameSave.PlayerId),
                IsTurn = userGameSave.IsTurn,

                //COMMENTED OUT FOR MANUAL INSERTION
                TotalDirectionHistory = userGameSave.TotalDirectionHistory,
                CurrentDirectionHistory = userGameSave.CurrentDirectionHistory,
                TotalCoordinateHistory = userGameSave.TotalCoordinateHistory,
                CurrentCoordinateHistory = userGameSave.CurrentCoordinateHistory,
                SystemsAttack = userGameSave.SystemsAttack,
                SystemsDetect = userGameSave.SystemsDetect,
                SystemsEvade = userGameSave.SystemsEvade,
                SystemsReactor = userGameSave.SystemsReactor,
                HealthValue = userGameSave.Health,
                MinesValue = userGameSave.Mines,
                DronesValue = userGameSave.Drones,
                SneakValue = userGameSave.Sneak,
                TorpedoValue = userGameSave.Torpedo,
                SonarValue = userGameSave.Sonar,

                //Temp direction history to help the front-end proccess
                //TotalDirectionHistory = "n,n,n,e,n,w,w,w,w,s",
                //CurrentDirectionHistory = "n,n,n,e,n,w,w,w,w,s",
                //TotalCoordinateHistory = "188,173,158,143,144,129,128,127,126,125,140",
                //CurrentCoordinateHistory = "188,173,158,143,144,129,128,127,126,125,140",
                //SystemsAttack = "true,false,true,true,false,false",
                //SystemsDetect = "true,false,false,true,true,false",
                //SystemsEvade = "false,false,true,false,false,true",
                //SystemsReactor = "true,false,false,false,true,false",


                //HEALTH = 3,
                //MINES = 3,
                //DRONES = 2,
                //SNEAK = 6,
                //TORPEDO = 0,
                //SONAR = 1,
                EnemyName = _playerService.GetPlayerName(opponentGameSave.PlayerId),

                //COMMENTED OUT FOR MANUAL INSERTION
                //EnemyDirectionHisory = opponentGameSave.TotalDirectionHistory,
                //EnemyCurrentDirectionHistory = opponentGameSave.CurrentDirectionHistory,

                //Temp direction history to help the front-end proccess
                EnemyDirectionHisory = "w,w,w,w,w,s,s,e,n",
                EnemyCurrentDirectionHistory = "w,w,w,w,w,s,s,e,n",
                EnemyHealth = opponentGameSave.Health,

                //Helper strings from Utilities/Helper
                //Power names
                Health = Helper.HEALTH,
                Mines = Helper.MINES,
                Drones = Helper.DRONES,
                Sneak = Helper.SNEAK,
                Torpedo = Helper.TORPEDO,
                Sonar = Helper.SONAR,
                AllCharged = Helper.ALL_CHARGED,

                //Directions
                West = Helper.WEST,
                North = Helper.NORTH,
                South = Helper.SOUTH,
                East = Helper.EAST,
                Surface = Helper.SURFACE,
                FirstMove = Helper.FIRST_MOVE
        };

            return gameVm;
            //throw new NotImplementedException();   
        }

        public string GenerateGameTerrain()
        {
            //TODO: fix this ugly ol code
            //possible error, what if it produces nothing?

            int[] terrainSeeds = new int[] { 32, 37, 42, 107, 112, 117, 182, 187, 192 };
            int[] innerRingOffsets = new int[] { -16, -15, -14, -1, 1, 14, 15, 16};
            int[] outerRingOffsets = new int[] { -32, -31, -30, -29, -28, -17, -13, -2, 2, 13, 17, 28, 29, 30, 31, 32};
            int terrainProbability = 4;
            int innerProbability = 2;
            int outerProbability = 1;
            string terrainList = "";
            int roll = 0;
            int actualTerrain = 0;

            Random rand = new Random();

            foreach (var item in terrainSeeds)
            {
                roll = rand.Next(11);
                if (roll > terrainProbability) { continue;  }

                terrainList += item.ToString() + ',';

                foreach (var inner in innerRingOffsets)
                {
                    roll = rand.Next(11);
                    if(roll > innerProbability) { continue; }
                    actualTerrain = inner + item;
                    terrainList += actualTerrain.ToString() + ',';
                }
                foreach (var outer in outerRingOffsets)
                {
                    roll = rand.Next(11);
                    if (roll > outerProbability) { continue; }
                    actualTerrain = outer + item;
                    terrainList += actualTerrain.ToString() + ',';
                }
            }
            //trim the extraneous ',' :)
            return terrainList.Substring(0, terrainList.Length - 2);
        }

        public bool DeleteGame(int id)
        {

            /* 
             *          Everywhere that a game needs to be deleted from  
             *
             *      * UserGame, GameId
             *      * Games, GameId
             *      * PlayerGameSaves, GameID
             */

            Console.WriteLine(id.ToString());

            Game game = _context.Games.FirstOrDefault(x => x.GameId == id) ?? throw new Exception("Could not find game to delete");
            UserGame userGame = _context.UserGames.FirstOrDefault(x => x.GameId == id) ?? throw new Exception("Could not find game to delete");
            List<PlayerGameSave> pgs = _context.PlayerGameSaves.Where(x => x.GameId == id).ToList();

            try
            {
                _context.Remove(game);
                _context.Remove(userGame);
                foreach (var item in pgs)
                {
                    _context.Remove(item);
                }
                _context.SaveChanges();

                return true;
            }
            catch(Exception ex)
            {
                throw new Exception();

                //return false;
            }
        }

        
    }
}