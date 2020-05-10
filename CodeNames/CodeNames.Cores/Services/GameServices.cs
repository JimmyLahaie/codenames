using CodeNames.Models.DTO;
using CodeNames.Models.Interfaces;

namespace CodeNames.Cores.Services
{
	public interface IGameServices
	{
		string CreateGame();
		Game GetGame(string id);
	}

	public class GameServices : IGameServices
	{
		private readonly IGameBuilder _gameBuilder;
		private readonly IGameRepository _gameRepository;

		public GameServices(IGameBuilder gameBuilder, IGameRepository gameRepository)
		{
			_gameBuilder = gameBuilder;
			_gameRepository = gameRepository;
		}

		public string CreateGame()
		{
			var newGame = _gameBuilder.GetNewGame();
			_gameRepository.Create(newGame);
			return newGame.Key;
		}

		public Game GetGame(string gameKey)
		{
			var game = _gameRepository.GetGame(gameKey);
			return game;
		}
	}
}