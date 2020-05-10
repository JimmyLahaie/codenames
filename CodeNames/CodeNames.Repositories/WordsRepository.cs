using System.Collections.Generic;
using System.Linq;
using CodeNames.Models.Interfaces;


namespace CodeNames.Repositories
{
	public class WordsRepository : IWordsRepository
	{
		private readonly IFileReader _fileReader;
		private readonly IRandomUtils _randomUtils;

		public WordsRepository(IFileReader fileReader, IRandomUtils randomUtils)
		{
			_fileReader = fileReader;
			_randomUtils = randomUtils;
		}

		public List<string> Get25RandomWords()
		{
			var lst = _fileReader.GetAllLines("");
			_randomUtils.RandomizeList(lst);
			return lst.Take(25).ToList();
		}
	}
}