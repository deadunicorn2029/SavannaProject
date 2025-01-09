using Savanna.Data;
using Savanna.Data.Repositories.Interfaces;
using Savanna.Services.Interfaces;
using SavannaWeb.Models;
using SavannaWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
//using SavannaWeb.Services;

namespace SavannaWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IApplicationDbContext _context;
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        //private readonly GameService _gameService;

        public HomeController(ILogger<HomeController> logger, IApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
            //_gameService = gameService;
        }

        public async Task<IActionResult> Welcome(string username)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public IActionResult Game()
        {
            return View();
        }

        //public IActionResult UpdateGame()
        //{
        //    var updatedAnimals = _gameService.MoveAnimals();
        //    return Json(new { animals = updatedAnimals });
        //}
    }
}
