using System;
using System.Collections.Generic;
using System.Linq;
using CodeNames.Cores.Models;
using CodeNames.Cores.Services;
using CodeNames.Cores.Utils;
using CodeNames.Interfaces;
using Xunit;
using FakeItEasy;

namespace CodeNames.Cores.Tests.Services
{
	public class GameBuilderTests
	{
		private readonly IRandomCodeGenerator _codeGenerator;
		private readonly GameBuilder _gameBuilder;

		public GameBuilderTests()
		{
			var wordBuilder = A.Fake<IWordsRepository>(); 
			_codeGenerator = A.Fake<IRandomCodeGenerator>();
			
			A.CallTo(() => wordBuilder.Get25RandomWords(false)).Returns(GetListOfWords());
			A.CallTo(() => _codeGenerator.GetRandomGameCode()).Returns("aaa");
			
			_gameBuilder = new GameBuilder(wordBuilder, _codeGenerator);
		}
		
		[Fact]
		public void BuildingNewGaShouldAssignRandomKey()
		{
			A.CallTo(() => _codeGenerator.GetRandomGameCode()).Returns("some-code");

			var game = _gameBuilder.GetNewGame(Color.Blue);
			Assert.Equal("some-code", game.Key);
		}

		[Fact]
		public void WhenBlueStartShouldHave9Blue8Red()
		{
			var game = _gameBuilder.GetNewGame(Color.Blue);
			var cards = GetGameCardList(game);
			Assert.Equal(9, cards.Count(c => c.Color == Color.Blue));
			Assert.Equal(8, cards.Count(c => c.Color == Color.Red));
			Assert.Equal(1, cards.Count(c => c.Color == Color.Black));
			Assert.Equal(7, cards.Count(c => c.Color == Color.Beige));
		}
		
		[Fact]
		public void WhenRedStartShouldHave9Red8Blue()
		{
			var game = _gameBuilder.GetNewGame(Color.Blue);
			var cards = GetGameCardList(game);
			Assert.Equal(9, cards.Count(c => c.Color == Color.Red));
			Assert.Equal(8, cards.Count(c => c.Color == Color.Blue));
			Assert.Equal(1, cards.Count(c => c.Color == Color.Black));
			Assert.Equal(7, cards.Count(c => c.Color == Color.Beige));
		}

		[Fact]
		public void TestAboutRandom()
		{
			Assert.True(false, "Should do test about random or create a randomutil and mock it");
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