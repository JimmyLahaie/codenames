using System;
using System.IO;
using System.Linq;
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

		public void AddHint(string key, string word, int number, Color player)
		{
			word = word.Replace(",", "").Replace(System.Environment.NewLine, "");
			File.AppendAllLines(GameChoicesFileName(key), new []{$"h,{(int)player},{word},{number}"});
		}

		public void AddChoice(string key, int x, int y, Color player)
		{
			File.AppendAllLines(GameChoicesFileName(key), new []{$"c,{(int)player},{x},{y}"});
		}

		public GameActions GetActions(string key)
		{
			var ga = new GameActions();
			var data = File.ReadAllLines(GameChoicesFileName(key)).Select(l => l.Split(',')).ToList();

			Func<string, Color> color = s => (Color) Convert.ToInt32(s);
			
			foreach (var line in data)
			{
				switch (line[0])
				{
					case "h":
						ga.Actions.Add(new HintAction{ActionFromType = PlayerType.SpyMaster, ActionFromColor = color(line[1]), Word = line[2], NbrCard = Convert.ToInt32(line[3])});
						break;
					case "c":
						ga.Actions.Add(new ChooseCardAction{ActionFromType = PlayerType.Agent, ActionFromColor = color(line[1]), X = Convert.ToInt32(line[2]), Y = Convert.ToInt32(line[3])});
						break;
					default:
						throw new Exception($"Unknow action '{line[0]}'");
				}
			}

			return ga;
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