using System;
using System.IO;
using CodeNames.Models.DTO;
using CodeNames.Models.Exceptions;
using DeepEqual.Syntax;
using Newtonsoft.Json;
using Xunit;

namespace CodeNames.Repositories.Tests
{
	public class GameRepositoryTests : IDisposable
	{
		private readonly GameRepository _repository;
		private readonly string _gameDir;

		public GameRepositoryTests()
		{
			_gameDir = Path.Combine(Directory.GetCurrentDirectory(), "Games") ;
			Directory.CreateDirectory(_gameDir);
			_repository = new GameRepository(_gameDir);
		}
		
		public void Dispose()
		{
			Directory.Delete(_gameDir, true);
		}

		[Fact]
		public void CanCreateGame()
		{
			var game = GivenAGame();
			
			_repository.Create(game);

			var gameFileName = Path.Combine(_gameDir, game.Key + ".txt");
			var choiceFileName = Path.Combine(_gameDir, game.Key + "-c.txt");
			
			Assert.True(File.Exists(gameFileName));
			Assert.True(File.Exists(choiceFileName));
			Assert.Equal(File.ReadAllText(gameFileName), JsonConvert.SerializeObject(game));
		}

		[Fact]
		public void CreateGameThrowsIfFileExists()
		{
			var game = GivenAGame();
			
			var gameFileName = Path.Combine(_gameDir, game.Key + ".txt");
			
			File.CreateText(gameFileName).Dispose();

			Assert.Throws<GameAlreadyExistsException>(() => _repository.Create(game));
		}

		[Fact]
		public void CanGetGame()
		{
			var game = GivenAGame();
			var gameFileName = Path.Combine(_gameDir, game.Key + ".txt");

			using (var file = File.CreateText(gameFileName))
			{
				file.Write(JsonConvert.SerializeObject(game));
			}

			var result = _repository.GetGame(game.Key);
			
			result.ShouldDeepEqual(game);
		}

		[Fact]
		public void GetGameThrowsIfGameDoesNotExits()
		{
			var gameKey = "someGameNotFound";

			var ex = Assert.Throws<GameNotFoundException>(() => _repository.GetGame(gameKey));
			Assert.Contains(gameKey, ex.Message);
			Assert.Contains(_gameDir, ex.Message);
		}

		private Game GivenAGame()
		{
			var game = new Game
			{
				Key = "newGame01",
				Cards = new[,] {{C(Color.Blue, "Aaa"), C(Color.Red, "Bbb")}},
				FirstPlayer = Color.Blue
			};
			return game;
		}

		private Card C(Color color, string word)
		{
			return new Card {Color = color, Word = word};
		}

		
	}
}