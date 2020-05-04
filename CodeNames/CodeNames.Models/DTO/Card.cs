namespace CodeNames.Models.DTO
{
	public class Card
	{
		public string Word { get; set; }
		public Color Color { get; set; }
		public bool Identified { get; set; }

		public Card()
		{
			Identified = false;
		}
	}
}