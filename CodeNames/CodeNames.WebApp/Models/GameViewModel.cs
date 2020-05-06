using CodeNames.Models.DTO;

namespace CodeNames.WebApp.Models
{
	public class GameViewModel
	{
		public string Key { get; set; }
		public CardViewModel[,] Cards { get; set; }

		public Color FirstPlayer { get; set; }

		public GameViewModel()
		{
			Cards = new CardViewModel[5,5];
		}

		public static GameViewModel FromGame(Game game)
		{
			var gameViewModel = new GameViewModel
			{
				FirstPlayer = game.FirstPlayer,
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