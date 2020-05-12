namespace CodeNames.Models.DTO
{
	public class ChoiceResult
	{
		public Color CardColor { get; set; }
		public bool TurnEnd { get; set; }
		public bool GameEnd => Winner.HasValue;
		public Color? Winner { get; set; }
		public bool LostByAssassin { get; set; }
		public int X { get; set; }
		public int Y { get; set; }
	}
}