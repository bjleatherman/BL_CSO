using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IdentityTutorial.Areas.Identity.Data;
using IdentityTutorial.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using IdentityTutorial.Services;
using System.Diagnostics.CodeAnalysis;

namespace IdentityTutorial.Controllers
{
    [Authorize]
    public class FriendsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IFriendService _friendService;
        private readonly IPlayerService _playerService;
        private readonly IGameService _gameService;

        public FriendsController(ApplicationDbContext context, IFriendService friendService, IPlayerService playerService, IGameService gameService)
        {
            _context = context;
            _friendService = friendService;
            _playerService = playerService;
            _gameService = gameService;
        }

        // GET: PlayerFriendVMs
        public async Task<IActionResult> Index()
        {
            // Get userId string, if not found return not found
            string userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value).ToString() ??
                string.Empty;

            if (userId == string.Empty) { return NotFound("UserId not found"); }

            return View(_friendService.GetFriendList(userId));
              //return View(await _context.PlayerFriendVM.ToListAsync());
        }

        // GET: PlayerFriendVMs/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.PlayerFriendVM == null)
        //    {
        //        return NotFound();
        //    }

        //    var playerFriendVM = await _context.PlayerFriendVM
        //        .FirstOrDefaultAsync(m => m.FriendId == id);
        //    if (playerFriendVM == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(playerFriendVM);
        //}

        // GET: PlayerFriendVMs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PlayerFriendVMs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FriendId,FriendName,IsConfirmed")] PlayerFriendVM playerFriendVM)
        {
            if (ModelState.IsValid)
            {
                _context.Add(playerFriendVM);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(playerFriendVM);
        }

        // GET: PlayerFriendVMs/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.PlayerFriendVM == null)
        //    {
        //        return NotFound();
        //    }

        //    var playerFriendVM = await _context.PlayerFriendVM.FindAsync(id);
        //    if (playerFriendVM == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(playerFriendVM);
        //}

        // POST: PlayerFriendVMs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FriendId,FriendName,IsConfirmed")] PlayerFriendVM playerFriendVM)
        {
            if (id != playerFriendVM.FriendId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playerFriendVM);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerFriendVMExists(playerFriendVM.FriendId))
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
            return View(playerFriendVM);
        }

        // GET: PlayerFriendVMs/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.PlayerFriendVM == null)
        //    {
        //        return NotFound();
        //    }

        //    var playerFriendVM = await _context.PlayerFriendVM
        //        .FirstOrDefaultAsync(m => m.FriendId == id);
        //    if (playerFriendVM == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(playerFriendVM);
        //}

        // POST: PlayerFriendVMs/Delete/5 
        // Delete the PlayerFriend records associated with the user and friend passed in
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string friendName)
        {

            // Get userId string, if not found return not found
            string userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value).ToString() ??
                string.Empty;

            if (userId == string.Empty) { return NotFound("UserId not found"); }

            if(_friendService.DeleteFriend(userId, friendName))
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound("Unable to find friend record");
            }
        }

        // POST: Check if the passed username exists in the database
        [HttpPost("/Search")]
        [ValidateAntiForgeryToken]
        public IActionResult Search([FromForm] string searchTerm)
        {
            // Get userId string, if not found return not found
            string userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value).ToString() ??
                string.Empty;

            if (userId == string.Empty) { return NotFound("UserId not found"); }

            return Json(_friendService.IsValidFriend(userId, searchTerm));
        }

        // POST: add passed username into player friends table
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddFriend(string searchTerm)
        {
            // Get userId string, if not found return not found
            string userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value).ToString() ??
                string.Empty;

            if (userId == string.Empty) { return NotFound("UserId not found"); }

            // If adding the friend to the friends list is  successful, return an updated view
            if (_friendService.AddFriend(userId, searchTerm)) 
            {
                return RedirectToAction("Index");
            } 
            else
            {
                return NotFound("The user was unable to be added as a friend");
            }
        }

        // POST: Accept Friend Request
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AcceptFriend(string friendName)
        {
            // Get userId string, if not found return not found
            string userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value).ToString() ??
                string.Empty;

            if (userId == string.Empty) { return NotFound("UserId not found"); }

            if (_friendService.AcceptFriendRequest(userId, friendName))
            {
                return RedirectToAction("Index");
            }
            else
            {
                throw new Exception("Accpet Friend Failed");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChallengeFriend(string friendName)
        {
            // Get userId string, if not found return not found
            string userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value).ToString() ??
                string.Empty;

            if (userId == string.Empty) { return NotFound("UserId not found"); }

            string friendId = _playerService.GetUserIdFromPlayerName(friendName);

            int newGameId = _gameService.CreateNewGame(userId, friendId);


            return RedirectToAction("Play", "Games",new { Id = newGameId });
        }

        private bool PlayerFriendVMExists(int id)
        {
          return _context.PlayerFriends.Any(e => e.Id == id);
        }
    }
}
