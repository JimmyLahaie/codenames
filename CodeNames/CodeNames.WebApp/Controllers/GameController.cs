using System.Web.Mvc;
using CodeNames.Cores.Models;
using CodeNames.Cores.Services;
using CodeNames.WebApp.Models;

namespace CodeNames.WebApp.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameBuilder _gameBuilder;

        public GameController(IGameBuilder gameBuilder)
        {
            _gameBuilder = gameBuilder;
        }
        
        public ActionResult Index()
        {
            var game = _gameBuilder.GetNewGame(Color.Blue);
            var gameViewModel = new GameViewModel
            {
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

            return View(gameViewModel);
        }

    }
}