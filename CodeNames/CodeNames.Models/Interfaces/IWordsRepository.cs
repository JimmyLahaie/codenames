using System.Collections.Generic;

namespace CodeNames.Models.Interfaces
{
	public interface IWordsRepository
	{
		List<string> Get25RandomWords();
	}
}