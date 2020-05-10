using System.Collections.Generic;
using System.Linq;
using CodeNames.Models.Interfaces;
using FakeItEasy;
using Xunit;

namespace CodeNames.Repositories.Tests
{
	public class WordsRepositoryTest
	{
		public WordsRepositoryTest()
		{
			_randomUtils = A.Fake<IRandomUtils>(x => x.Strict());
			_fileReader = A.Fake<IFileReader>(x => x.Strict());
			_repo = new WordsRepository(_fileReader, _randomUtils);
		}

		private readonly WordsRepository _repo;
		private readonly IRandomUtils _randomUtils;
		private readonly IFileReader _fileReader;

		private static List<string> GivenAListOfWords()
		{
			var wordList = new List<string>();
			for (var i = 0; i < 30; i++) wordList.Add(i.ToString());

			return wordList;
		}

		[Fact]
		public void CanGetRandomWords()
		{
			var wordList = GivenAListOfWords();
			var expectedList = wordList.Take(25).ToList();
			expectedList[0] = "999";
			A.CallTo(() => _fileReader.GetAllLines(A<string>.Ignored)).Returns(wordList);

			A.CallTo(() => _randomUtils.RandomizeList(wordList)).Invokes(() => { wordList[0] = "999"; });
			var result = _repo.Get25RandomWords();
			Assert.Equal(expectedList, result);
		}

		[Fact]
		public void MustHave25Words()
		{
			var wordList = GivenAListOfWords();

			A.CallTo(() => _fileReader.GetAllLines(A<string>.Ignored)).Returns(wordList);
			A.CallTo(() => _randomUtils.RandomizeList(wordList)).Invokes(() => { });

			var words = _repo.Get25RandomWords();
			Assert.Equal(25, words.Count);
		}
	}
}