using CodeNames.Cores.Models;

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
	}

	public class CardViewModel
	{
		public string Word { get; set; }
		public Color Color { get; set; }
		public bool Identified { get; set; }
	}
}