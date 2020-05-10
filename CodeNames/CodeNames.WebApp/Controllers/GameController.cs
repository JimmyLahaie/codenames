using System.Web.Mvc;
using CodeNames.Cores.Services;
using CodeNames.Models.DTO;
using CodeNames.WebApp.Models;

namespace CodeNames.WebApp.Controllers
{
	public class GameController : Controller
	{
		private readonly IGameServices _gameService;

		public GameController(IGameServices gameService)
		{
			_gameService = gameService;
		}

		public ActionResult SpyMaster(string key)
		{
			var game = _gameService.GetGame(key);

			var gameViewModel = GameViewModel.FromGame(game);

			return View("SpyMaster", gameViewModel);
		}

		public ActionResult Agent(string key)
		{
			var game = _gameService.GetGame(key);

			var gameViewModel = GameViewModel.FromGame(game);

			return View("Agent", gameViewModel);
		}

		[HttpPost]
		public ActionResult New(PlayerType playerType)
		{
			var gameKey = _gameService.CreateGame();
			return RedirectToAction(playerType == PlayerType.SpyMaster ? "SpyMaster" : "Agent", new {key = gameKey});
		}
	}
}