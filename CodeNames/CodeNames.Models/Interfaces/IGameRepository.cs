using CodeNames.Models.DTO;

namespace CodeNames.Models.Interfaces
{
	public interface IGameRepository
	{
		void Create(Game game);
		Game GetGame(string key);
	}
}