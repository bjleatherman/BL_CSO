using IdentityTutorial.Areas.Identity.Data;
using IdentityTutorial.Models;
using IdentityTutorial.Models.ViewModels;
using IdentityTutorial.Services;

namespace IdentityTutorial.Utilities
{
    public class TurnData
    {
        private readonly ITurnService _turnService;
        private readonly IPlayerService _playerService;

        public PlayerGameSave _playerGameSave;
        public Game _game;
        public Player _player;
        public Dictionary<string, int>[] powerState;
        public Dictionary<string, bool[]> systemsState;
        public SystemsModel[] systemsModels;
        public TurnVM turn;
        public TurnData(ITurnService turnService, IPlayerService playerService, TurnVM turn) 
        { 
            _turnService = turnService;
            _playerService = playerService;
            _playerGameSave = _turnService.GetPlayerGameSave(turn);
            _game= _turnService.GetGame(turn);
            _player = _playerService.GetPlayer(turn.UserId);
            powerState = _turnService.GetPowerState(_playerGameSave, _game);
            systemsState = _turnService.GetSystemsState(_playerGameSave);
            systemsModels = _turnService.PopulateIsFixedInModel(Helper.GetSystemsModel(), systemsState);
            this.turn = turn;
        }
    }
}
