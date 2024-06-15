using IdentityTutorial.Areas.Identity.Data;
using IdentityTutorial.Models;
using IdentityTutorial.Models.ViewModels;
using IdentityTutorial.Utilities;

namespace IdentityTutorial.Services
{
    public interface ITurnService
    {
        public bool SubmitPlayerTurn(Models.ViewModels.TurnVM turn);
        public PlayerGameSave GetPlayerGameSave(TurnVM turn);
        public Game GetGame(TurnVM turn);
        public Dictionary<string, int>[] GetPowerState(PlayerGameSave _playerGameSave, Game _game);
        public Dictionary<string, bool[]> GetSystemsState(PlayerGameSave _playerGameSave);
        public SystemsModel[] PopulateIsFixedInModel(SystemsModel[] systemsModels, Dictionary<string, bool[]> systemsState);
    }
}
