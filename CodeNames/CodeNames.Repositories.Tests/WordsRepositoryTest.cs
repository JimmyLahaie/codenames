using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xunit;

namespace CodeNames.Repositories.Tests
{
	public class WordsRepositoryTest
	{
		private WordsRepository _repo;

		public WordsRepositoryTest()
		{
			_repo = new WordsRepository();
		}
		
		[Fact]
		public void CanGetRandomWords()
		{
			//Yes, this is an empirical test
			var lstOfList = new List<List<string>>();
			
			for (int i = 0; i < 10; i++)
			{
				lstOfList.Add(_repo.Get25RandomWords(false));
			}

			for (int i = 0; i < 9; i++)
			{
				for (int j = i+1; j < 10; j++)
				{
					Assert.NotEqual(lstOfList[i], lstOfList[j]);
				}
			}
		}

		[Fact]
		public void MustHave25Words()
		{
			var words= _repo.Get25RandomWords(false);
			Assert.Equal(25, words.Count);
		}
	}
}