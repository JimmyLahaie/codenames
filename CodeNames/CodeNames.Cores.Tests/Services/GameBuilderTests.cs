using System.Collections.Generic;
using System.Linq;
using CodeNames.Cores.Services;
using CodeNames.Models.DTO;
using CodeNames.Models.Interfaces;
using FakeItEasy;
using Xunit;

namespace CodeNames.Cores.Tests.Services
{
	public class GameBuilderTests
	{
		public GameBuilderTests()
		{
			var wordBuilder = A.Fake<IWordsRepository>(o => o.Strict());
			_randomUtils = A.Fake<IRandomUtils>(o => o.Strict());

			A.CallTo(() => wordBuilder.Get25RandomWords()).Returns(GetListOfWords());
			A.CallTo(() => _randomUtils.GenerateCode()).Returns("aaa");
			A.CallTo(() => _randomUtils.SingleValue(Color.Blue, Color.Red)).Returns(Color.Blue);
			A.CallTo(() => _randomUtils.RandomizeList(A<List<Card>>.Ignored)).Invokes(() => { });

			_gameBuilder = new GameBuilder(wordBuilder, _randomUtils);
		}

		private readonly IRandomUtils _randomUtils;
		private readonly GameBuilder _gameBuilder;

		[Theory]
		[InlineData(Color.Blue)]
		[InlineData(Color.Red)]
		public void FirstPlayerMustHave9Cards(Color firstPlayer)
		{
			var otherPlayer = firstPlayer == Color.Blue ? Color.Red : Color.Blue;
			A.CallTo(() => _randomUtils.SingleValue(A<Color>.Ignored, A<Color>.Ignored)).Returns(firstPlayer);
			var game = _gameBuilder.GetNewGame();
			var cards = GetGameCardList(game);
			Assert.Equal(9, cards.Count(c => c.Color == firstPlayer));
			Assert.Equal(8, cards.Count(c => c.Color == otherPlayer));
			Assert.Equal(1, cards.Count(c => c.Color == Color.Black));
			Assert.Equal(7, cards.Count(c => c.Color == Color.Beige));
		}

		private List<Card> GetGameCardList(Game game)
		{
			var lst = new List<Card>();
			for (var i = 0; i < 5; i++)
			for (var j = 0; j < 5; j++)
				lst.Add(game.Cards[i, j]);

			return lst;
		}

		private List<string> GetListOfWords()
		{
			var lst = new List<string>();
			for (var i = 0; i < 25; i++) lst.Add("w-" + i);

			return lst;
		}

		[Fact]
		public void BuildingNewGaShouldAssignRandomKey()
		{
			A.CallTo(() => _randomUtils.GenerateCode()).Returns("some-code");

			var game = _gameBuilder.GetNewGame();
			Assert.Equal("some-code", game.Key);
		}

		[Fact]
		public void ColorShouldBeShuffle()
		{
			_gameBuilder.GetNewGame();
			A.CallTo(() => _randomUtils.RandomizeList(A<List<Card>>.Ignored)).MustHaveHappened();
		}
	}
}