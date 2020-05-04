using System.Data;
using System.IO;
using CodeNames.Models.DTO;
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
			var filename = GameFullFileName(game.Key);
			if (File.Exists(filename))
			{
				throw new DuplicateNameException($"Cannot create game with key '{game.Key}'. Another game with the same key already exists.");
			}

			var gameAsJson = JsonConvert.SerializeObject(game);
			using (var file = File.CreateText(filename))
			{
				file.Write(gameAsJson);
			}
			
			File.CreateText(GameChoicesFileName(game.Key)).Dispose();
		}

		public Game GetGame(string key)
		{
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