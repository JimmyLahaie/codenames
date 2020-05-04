using System.Web.Mvc;
using CodeNames.Cores.Models;
using CodeNames.Cores.Services;
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
        
        public ActionResult SpyMaster(string id)
        {
            var game = _gameService.GetGame(id);
            
            var gameViewModel = new GameViewModel
            {
                FirstPlayer = game.FirstPlayer,
                Key = game.Key
            };
            
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    var card = game.Cards[i, j];
                    gameViewModel.Cards[i, j] = new CardViewModel{Color = card.Color, Word = card.Word, Identified = card.Identified};
                }
            }

            return View("Index", gameViewModel);
        }

        [HttpPost]
        public ActionResult New(PlayerType playerType)
        {
            var gameKey = _gameService.CreateGame();
            return RedirectToAction(playerType == PlayerType.SpyMaster ? "SpyMaster" : "Agent", new {id = gameKey});
        }

    }
}