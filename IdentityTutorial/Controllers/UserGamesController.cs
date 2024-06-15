using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IdentityTutorial.Areas.Identity.Data;
using IdentityTutorial.Models;
using IdentityTutorial.Models.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Collections;
using IdentityTutorial.Services;

namespace IdentityTutorial.Controllers
{
    [Authorize]
    public class UserGamesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserGameService _userGameService;

        public UserGamesController(ApplicationDbContext context, IUserGameService userGameService)
        {
            _context = context;
            _userGameService = userGameService;
        }

        // GET: UserGames
        public async Task<IActionResult> Index()
        {
              //return _context.UserGames != null ? 
              //            View(await _context.UserGames.ToListAsync()) :
              //            Problem("Entity set 'ApplicationDbContext.UserGames'  is null.");

            if(_context.UserGames != null)
            {
                try
                {
                    var userId = Guid.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier).Value).ToString();

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
                return Problem("Entity set 'ApplicationDbContext.TestTable'  is null.");
            }
        }

        // GET: UserGames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserGames == null)
            {
                return NotFound();
            }

            var userGame = await _context.UserGames
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userGame == null)
            {
                return NotFound();
            }

            return View(userGame);
        }

        // GET: UserGames/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserGames/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,User1Id,User2Id,GameId")] UserGame userGame)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userGame);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userGame);
        }

        // GET: UserGames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserGames == null)
            {
                return NotFound();
            }

            var userGame = await _context.UserGames.FindAsync(id);
            if (userGame == null)
            {
                return NotFound();
            }
            return View(userGame);
        }

        // POST: UserGames/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,User1Id,User2Id,GameId")] UserGame userGame)
        {
            if (id != userGame.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userGame);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserGameExists(userGame.Id))
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
            return View(userGame);
        }

        // GET: UserGames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserGames == null)
            {
                return NotFound();
            }

            var userGame = await _context.UserGames
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userGame == null)
            {
                return NotFound();
            }

            return View(userGame);
        }

        // POST: UserGames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserGames == null)
            {
                return Problem("Entity set 'ApplicationDbContext.UserGames'  is null.");
            }
            var userGame = await _context.UserGames.FindAsync(id);
            if (userGame != null)
            {
                _context.UserGames.Remove(userGame);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserGameExists(int id)
        {
          return (_context.UserGames?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
