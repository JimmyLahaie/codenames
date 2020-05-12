using CodeNames.Models.DTO;

namespace CodeNames.Models.Interfaces
{
	public interface IGameRepository
	{
		void Create(Game game);
		Game GetGame(string key);
		void AddHint(string key, string word, int number, Color player);
		void AddChoice(string key, int x, int y, Color player);
		GameActions GetActions(string key);
	}
}