using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IdentityTutorial.Areas.Identity.Data;
using IdentityTutorial.Models;
using IdentityTutorial.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using IdentityTutorial.Models.ViewModels;
using IdentityTutorial.Utilities;

namespace IdentityTutorial.Controllers
{
    [Authorize]
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IGameService _gameService;
        private readonly IUserGameService _userGameService;
        private readonly ITurnService _turnService;

        public GamesController(ApplicationDbContext context, IGameService gameService, IUserGameService userGameService, ITurnService turnService)
        {
            _context = context;
            _gameService = gameService;
            _userGameService = userGameService;
            _turnService = turnService;
        }

        //GET Games Player
        public async Task<IActionResult> Play(int id)
        {
            //TODO: find a way to make the int param int? again
            if (id == null || 
                _context.Games == null || 
                Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value).ToString() == null)
            {
                Console.WriteLine("No ID PASSED-------------------------------------------------------------------------------------------");
                return NotFound();
            }

            string userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value).ToString();
            var gameVm = _gameService.GetGameVM(id, userId);

            if (gameVm == null)
            {
                return NotFound();
            }

            return View(gameVm);
        }

        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Play(int id, 
            string direction,
            string systems, 
            string power, 
            int? firstMoveLocation,
            string? activatedPower,
            int? activatedPowerValue, 
            string? message,
            int? startingMove, 
            int? sonarTrueType, 
            int? sonarTrueValue, 
            int? sonarFalseType, 
            int? sonarFalseValue)
        {
            /*
             *                      Valid Turn Contains:
             *          Mandatory
             *              * game id
             *              * direction (can be surface)
             *              * damage to a system (can be surface)
             *              * increase power (can be 'powers full')
             *          Optional
             *              * use a power
             *              * send a message
             *              * make a starting move
             *              * respond to sonar request
             *                  - true value
             *                      ~ type of value (quad, col, row)
             *                      ~ value (1-15)
             *                  - false value
             *                      ~ type of value (quad, col, row)
             *                      ~ value (1-15)
             */
            
            Console.WriteLine($"=======================================================\r\n" +
                $"id: {id} \r\n" +
                $"direction: {direction} \r\n" +
                $"System Button: {systems} \r\n" +
                $"Use Power: {power} \r\n" +
                $"First Move at: {firstMoveLocation} \r\n" +
                $"Activating Power: {activatedPower}\r\n" +
                $"Activated Power Value: {activatedPowerValue}\r\n" +
                $"Message Sent: {message} \r\n" +
                $"Starting Location: {startingMove} \r\n" +
                $"True Sonar Type: {sonarTrueType} \r\n" +
                $"True Sonar Value: {sonarTrueValue} \r\n" +
                $"False Sonar Type: {sonarFalseType} \r\n" +
                $"False Sonar Value: {sonarFalseValue} \r\n" +
                $"=======================================================");

            string userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value).ToString();

            if(direction != Helper.FIRST_MOVE)
            {
                PlayerGameSave _playerGameSave = _context.PlayerGameSaves?.FirstOrDefault(
                    x => x.GameId == id &&
                    x.PlayerId == userId)
                    ?? new PlayerGameSave();

                string[] totalCoordinateHistory = _playerGameSave.TotalCoordinateHistory.Split(',', StringSplitOptions.RemoveEmptyEntries);

                Console.WriteLine("==============================");
                Console.WriteLine(totalCoordinateHistory[0]);
                Console.WriteLine(totalCoordinateHistory.Length);
                Console.WriteLine(totalCoordinateHistory[totalCoordinateHistory.Length - 1]);

                int newMove = Convert.ToInt32(totalCoordinateHistory[totalCoordinateHistory.Length-1]);

                Console.WriteLine($"current location Move: {newMove}");

                switch (direction)
                {
                    case Helper.WEST: newMove = newMove - 1; break;
                    case Helper.NORTH: newMove = newMove - 15; break;
                    case Helper.SOUTH: newMove = newMove + 15; break;
                    case Helper.EAST: newMove = newMove + 1; break;
                }

                _playerGameSave.TotalCoordinateHistory += newMove.ToString() + ',';
                _playerGameSave.CurrentCoordinateHistory += newMove.ToString() + ',';

                _context.Update(_playerGameSave);
                _context.SaveChanges();
            }

            TurnVM turn = new()
            {
                GameId = id,
                UserId = userId,
                Direction = direction,
                Systems = systems,
                Power = power,
                FirstMoveLocation = firstMoveLocation,
                ActivatedPower = activatedPower,
                ActivedPowerValue = activatedPowerValue,
                Message = message,
                StartingMove = startingMove,
                SonarTrueType = sonarTrueType,
                SonarTrueValue = sonarTrueValue,
                SonarFalseType = sonarFalseType,
                SonarFalseValue = sonarFalseValue
            };

            _turnService.SubmitPlayerTurn(turn);

            return RedirectToAction(nameof(Play), id);
        }

        // GET: Games Index
        public async Task<IActionResult> Index()
        {

            if (_context.UserGames != null)
            {
                try
                {
                    var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value).ToString();

                    var results = _userGameService.GetUserGameVM(userId);

                    return View(results);
                }
                catch (Exception ex)
                {
                    return Problem(ex.ToString());
                }
            }
            else
            {
                return Problem("Entity set 'ApplicationDbContext.UserGameVM'  is null.");
            }


            //{
            //      return _context.Games != null ? 
            //                  View(await _context.Games.ToListAsync()) :
            //                  Problem("Entity set 'ApplicationDbContext.Games'  is null.");
            //}
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Games == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .FirstOrDefaultAsync(m => m.GameId == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            /**
             * THIS IS THE VIEW THAT THE USER WILL SEE
             * 
             * Creates new game when selected by user in UserGamesVM
             * 
             * Create GameService & IGameService to handle the game creation and call them here
             *
             */
            return View(); //I dont know if I'll need a new model in the view or not
        }

        // POST: Games/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParamInt,ParamString,ParamStringTwo,StartDate")] Game game)
        {
            //if (ModelState.IsValid)
            //{
            //    _context.Add(game);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}

            //Console.WriteLine("model state was not valid");
            //return View(game); //I dont know if I'll need a new model in the view or not. Maybe send user to the game that was just created?

            /**
             * THIS IS WHERE THE MODEL IS UPDATED | I haven't made any changes yet
             * 
             * Creates new game when selected by user in UserGamesVM
             * 
             * Create GameService & IGameService to handle the game creation and call them here
             */
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value).ToString();

            //TODO: error handling and async 
            int newGameId = _gameService.CreateNewGame(userId);
            return RedirectToAction(nameof(Play), new { Id = newGameId });
            //return View();
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Games == null)
            {
                return NotFound();
            }

            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GameId,ParamInt,ParamString,ParamStringTwo,StartDate")] Game game)
        {
            if (id != game.GameId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.GameId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Games == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .FirstOrDefaultAsync(m => m.GameId == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int gameId)
        {
            //if (_context.Games == null)
            //{
            //    return Problem("Entity set 'ApplicationDbContext.Games'  is null.");
            //}
            //var game = await _context.Games.FindAsync(id);
            //if (game != null)
            //{
            //    _context.Games.Remove(game);
            //}

            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));

            if (_gameService.DeleteGame(gameId)) {
                return RedirectToAction(nameof(Index));
            }
            else 
            {
                return Problem("Could not Delete Game");
            }

        }

        private bool GameExists(int id)
        {
          return (_context.Games?.Any(e => e.GameId == id)).GetValueOrDefault();
        }
    }
}
