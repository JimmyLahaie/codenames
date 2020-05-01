using System.Collections.Generic;
using System.Linq;

namespace CodeNames.Repositories
{
	public interface IFileReader
	{
		List<string> GetAllLines(string file);
	}

	public class FileReader : IFileReader
	{
		public List<string> GetAllLines(string file)
		{
			return new[]
			{
				"soldat",
				"ballon",
				"nain",
				"mode",
				"neige",
				"don",
				"bar",
				"plan",
				"guide",
				"château",
				"appareil",
				"main",
				"jour",
				"hôpital",
				"pigeon",
				"cirque",
				"poingouin",
				"plume",
				"espion",
				"jet",
				"étude",
				"kiwi",
				"bête",
				"iris",
				"satellite",
				"cinéma",
				"flûte",
				"docteur",
				"voiture",
				"poisson"
			}.ToList();
		}
	}
}