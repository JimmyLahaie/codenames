namespace CodeNames.Models.DTO
{
    public class Game
    {
        public string Key { get; set; }
        public Card[,] Cards { get; set; }

        public Color FirstPlayer { get; set; }
        public Game()
        {
            Cards = new Card[5,5];
        }
    }
}