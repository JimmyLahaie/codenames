namespace CodeNames.Cores.Models
{
    public class Game
    {
        public string Key { get; set; }
        public Card[,] Cards { get; set; }

        public Game()
        {
            Cards = new Card[5,5];
        }
    }

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

    public enum Color
    {
        Red,
        Blue,
        Black,
        Beige
    }
}