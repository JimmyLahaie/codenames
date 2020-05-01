﻿using System.Collections.Generic;
using System.Linq;
using CodeNames.Cores.Models;
using CodeNames.Cores.Services;
using CodeNames.Interfaces;
using Xunit;
using FakeItEasy;

namespace CodeNames.Cores.Tests.Services
{
	public class GameBuilderTests
	{
		private readonly IRandomUtils _randomUtils;
		private readonly GameBuilder _gameBuilder;

		public GameBuilderTests()
		{
			var wordBuilder = A.Fake<IWordsRepository>(); 
			_randomUtils = A.Fake<IRandomUtils>();
			
			A.CallTo(() => wordBuilder.Get25RandomWords()).Returns(GetListOfWords());
			A.CallTo(() => _randomUtils.GenerateCode()).Returns("aaa");
			
			_gameBuilder = new GameBuilder(wordBuilder, _randomUtils);
		}
		
		[Fact]
		public void BuildingNewGaShouldAssignRandomKey()
		{
			A.CallTo(() => _randomUtils.GenerateCode()).Returns("some-code");

			var game = _gameBuilder.GetNewGame();
			Assert.Equal("some-code", game.Key);
		}

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
			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					lst.Add(game.Cards[i, j]);
				}	
			}

			return lst;
		}
		
		

		private List<string> GetListOfWords()
		{
			var lst = new List<string>();
			for (int i = 0; i < 25; i++)
			{
				lst.Add("w-" + i);
			}

			return lst;
		}
	}
}