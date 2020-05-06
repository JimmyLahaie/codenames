using CodeNames.Cores.Services;
using CodeNames.Models.DTO;
using CodeNames.Models.Interfaces;
using FakeItEasy;
using Xunit;

namespace CodeNames.Cores.Tests.Services
{
	public class GameServicesTests
	{
		private readonly IGameBuilder _gameBuilder;
		private readonly IGameRepository _gameRepository;
		private readonly GameServices _gameService;

		public GameServicesTests()
		{
			_gameBuilder = A.Fake<IGameBuilder>();
			_gameRepository = A.Fake<IGameRepository>();
			_gameService = new GameServices(_gameBuilder, _gameRepository);
		}
		
		[Fact]
		public void CanGetGame()
		{
			var game = new Game();
			var gameKey = "abcd";
			A.CallTo(() => _gameRepository.GetGame(gameKey)).Returns(game);

			var result = _gameService.GetGame(gameKey);
			Assert.Equal(game, result);
		}

		[Fact]
		public void CanCreateNewGame()
		{
			var newGame = new Game{Key = "ng"};
			A.CallTo(() => _gameBuilder.GetNewGame()).Returns(newGame);

			var result = _gameService.CreateGame();

			A.CallTo(() => _gameRepository.Create(newGame)).MustHaveHappened();
			Assert.Equal(newGame.Key, result);
		}
	}
}