using System.Collections.Generic;
using CodeNames.Interfaces;
using CodeNames.Models.DTO;

namespace CodeNames.Cores.Services
{
	public interface IGameBuilder
	{
		Game GetNewGame();
	}

	public class GameBuilder : IGameBuilder
	{
		private readonly IWordsRepository _wordRepository;
		private readonly IRandomUtils _randomUtils;


		public GameBuilder(IWordsRepository wordRepository, IRandomUtils randomUtils)
		{
			_wordRepository = wordRepository;
			_randomUtils = randomUtils;
		}
		
		public Game GetNewGame()
		{ 
			var game = new Game
			{
				FirstPlayer = _randomUtils.SingleValue(Color.Blue, Color.Red),
				Key = _randomUtils.GenerateCode()
			};
			var words = _wordRepository.Get25RandomWords();
			var cards = GetListOfCards(game.FirstPlayer, words);
			
			_randomUtils.RandomizeList(cards);
			
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