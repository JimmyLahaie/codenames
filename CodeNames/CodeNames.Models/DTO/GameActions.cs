using System.Collections.Generic;

namespace CodeNames.Models.DTO
{
	public class GameActions
	{
		public List<IAction> Actions { get; }

		public GameActions()
		{
			Actions = new List<IAction>();
		}
	}
	
	public interface IAction
	{
		PlayerType ActionFromType { get; set; }
		Color ActionFromColor { get; set; }
	}

	public class HintAction : IAction
	{
		public PlayerType ActionFromType { get; set; }
		public Color ActionFromColor { get; set; }
		public string Word { get; set; }
		public int NbrCard { get; set; }
	}

	public class ChooseCardAction : IAction
	{
		public PlayerType ActionFromType { get; set; }
		public Color ActionFromColor { get; set; }
		public int X { get; set; }
		public int Y { get; set; }
	}
}