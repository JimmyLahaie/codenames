using System.Collections.Generic;
using CodeNames.Models.DTO;

namespace CodeNames.WebApp.Models
{
	public class GameViewModel
	{
		private List<CardViewModel> _cardList;

		public GameViewModel()
		{
			Cards = new CardViewModel[5, 5];
		}

		public string Key { get; set; }
		public CardViewModel[,] Cards { get; }

		public List<CardViewModel> CardList
		{
			get
			{
				if (_cardList == null)
				{
					_cardList = new List<CardViewModel>();
					for (var i = 0; i < 5; i++)
					for (var j = 0; j < 5; j++)
						_cardList.Add(Cards[i, j]);
				}

				return _cardList;
			}
		}

		public Color FirstPlayer { get; set; }

		public static GameViewModel FromGame(Game game)
		{
			var gameViewModel = new GameViewModel
			{
				FirstPlayer = game.FirstPlayer,
				Key = game.Key
			};

			for (var i = 0; i < 5; i++)
			for (var j = 0; j < 5; j++)
			{
				var card = game.Cards[i, j];
				gameViewModel.Cards[i, j] = new CardViewModel
					{Color = card.Color, Word = card.Word, Identified = card.Identified};
			}

			return gameViewModel;
		}
	}

	public class CardViewModel
	{
		public string Word { get; set; }
		public Color Color { get; set; }
		public bool Identified { get; set; }
	}
}