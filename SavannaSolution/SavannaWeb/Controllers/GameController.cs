using Microsoft.AspNetCore.Mvc;
using SavannaWeb.ViewModels;
using Savanna.Services.Interfaces;

namespace SavannaWeb.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        // This action fetches animals and passes them to the view
        public async Task<IActionResult> StartGame()
        {
            var animals = await _gameService.GetInitialAnimals();

            var viewModel = new GameViewModel
            {
                Animals = animals.Select(a => new AnimalViewModel
                {
                    Type = a.AnimalType,
                    X = a.X,
                    Y = a.Y
                }).ToList()
            };

            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
