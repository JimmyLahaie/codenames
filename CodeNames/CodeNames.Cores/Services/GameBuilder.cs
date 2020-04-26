using System;
using System.Collections.Generic;
using CodeNames.Cores.Models;
using CodeNames.Cores.Utils;
using CodeNames.Interfaces;

namespace CodeNames.Cores.Services
{
	public interface IGameBuilder
	{
		Game GetNewGame(Color firstPlayer);
	}

	public class GameBuilder : IGameBuilder
	{
		private readonly IWordsRepository _wordRepository;
		private readonly IRandomCodeGenerator _randomCodeGenerator;

		public GameBuilder(IWordsRepository wordRepository, IRandomCodeGenerator randomCodeGenerator)
		{
			_wordRepository = wordRepository;
			_randomCodeGenerator = randomCodeGenerator;
		}
		
		public Game GetNewGame(Color firstPlayer)
		{
			if (firstPlayer == Color.Black || firstPlayer == Color.Beige)
			{
				throw new ArgumentException("First player cannot only be Blue or Red.");
			}
			
			var words = _wordRepository.Get25RandomWords(false);
			var cards = GetListOfCards(firstPlayer, words);
			var game = new Game
			{
				Key = _randomCodeGenerator.GetRandomGameCode()
			};
			
			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					game.Cards[i, j] = cards[i + (j * 5)];
				}
			}

			return game;
		}

		private static List<Card> GetListOfCards(Color firstPlayer, List<string> words)
		{
			List<Card> cards = new List<Card>();
			for (int i = 0; i < 9; i++)
			{
				cards.Add(new Card
				{
					Color = firstPlayer,
					Word = words[i]
				});
			}

			var otherPlayer = firstPlayer == Color.Blue ? Color.Red : Color.Blue;
			for (int i = 9; i < 17; i++)
			{
				cards.Add(new Card
				{
					Color = otherPlayer,
					Word = words[i]
				});
			}

			cards.Add(new Card {Color = Color.Black, Word = words[17]});
			for (int i = 18; i < 25; i++)
			{
				cards.Add(new Card {Color = Color.Beige, Word = words[i]});
			}

			return cards;
		}
	}
}