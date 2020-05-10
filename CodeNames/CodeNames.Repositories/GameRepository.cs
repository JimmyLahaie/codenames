using System.IO;
using CodeNames.Models.DTO;
using CodeNames.Models.Exceptions;
using CodeNames.Models.Interfaces;
using Newtonsoft.Json;

namespace CodeNames.Repositories
{
	public class GameRepository : IGameRepository
	{
		private readonly string _baseFolder;

		public GameRepository(string baseFolder)
		{
			_baseFolder = baseFolder;
		}

		public void Create(Game game)
		{
			var key = game.Key.ToUpper();
			var filename = GameFullFileName(key);
			if (File.Exists(filename)) throw new GameAlreadyExistsException(key, _baseFolder);

			var gameAsJson = JsonConvert.SerializeObject(game);
			using (var file = File.CreateText(filename))
			{
				file.Write(gameAsJson);
			}

			File.CreateText(GameChoicesFileName(key)).Dispose();
		}

		public Game GetGame(string key)
		{
			key = key.ToUpper();
			var filename = GameFullFileName(key);
			if (!File.Exists(filename)) throw new GameNotFoundException(key, _baseFolder);
			var json = File.ReadAllText(GameFullFileName(key));
			return JsonConvert.DeserializeObject<Game>(json);
		}

		private string GameFullFileName(string key)
		{
			return Path.Combine(_baseFolder, key + ".txt");
		}

		private string GameChoicesFileName(string key)
		{
			return Path.Combine(_baseFolder, key + "-c.txt");
		}
	}
}