using IdentityTutorial.Areas.Identity.Data;
using IdentityTutorial.Models;
using IdentityTutorial.Models.ViewModels;

namespace IdentityTutorial.Services
{
    public class UserGameService : IUserGameService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPlayerService _playerService;

        public UserGameService(ApplicationDbContext context, IPlayerService payerService)
        {
            _context = context;
            _playerService = payerService;
        }

        public UserGame CreateUserGame(int gameId, string player1Id, string player2Id)
        {
            //TODO: comment and make async
            UserGame userGame = new UserGame()
            {
                User1Id = player1Id,
                User2Id = player2Id,
                GameId = gameId
            };

            return userGame;
        }

        public List<UserGameVM> GetUserGameVM(string userId)
        {
            //TODO: comment and make async
            var _userGames = _context.UserGames.ToList();
            var _games = _context.Games.ToList();

            var userGameVm = _userGames
                .Where(x => x.User1Id == userId || x.User2Id == userId)
                .Join(
                        _games,
                        _userGames => _userGames.GameId,
                        _games => _games.GameId,
                        (_userGames, _games) => new UserGameVM
                        {
                            Id = _userGames.Id,
                            Player1 = _userGames.User1Id == userId ? 
                                _playerService.GetPlayerName(_userGames.User1Id) : 
                                _playerService.GetPlayerName(_userGames.User2Id),
                            Player2 = _userGames.User2Id == userId ? 
                                _playerService.GetPlayerName(_userGames.User1Id) : 
                                _playerService.GetPlayerName(_userGames.User2Id),
                            GameId = _userGames.GameId,
                            StartDate = _games.StartDate
                        }).ToList();

            return userGameVm;
        }
    }
}