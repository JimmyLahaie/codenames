using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CodeNames.Utils.Tests
{
	public class RandomUtilsTests
	{
		private readonly RandomUtils _random;

		public RandomUtilsTests()
		{
			_random = new RandomUtils();
		}
		
		[Fact]
		public void CanRandomizeList()
		{
			var lst = Enumerable.Range(1, 50).ToList();

			var randomLists = new List<List<int>>();
			
			for (int i = 0; i < 20; i++)
			{
				_random.RandomizeList(lst);
				randomLists.Add(lst.Select(x => x).ToList());
			}

			AssertAllDifferent(randomLists);
		}

		[Fact]
		public void CanGenerateCode()
		{
			var lst = Enumerable.Range(0, 50).Select(x => _random.GenerateCode()).ToList();

			AssertAllDifferent(lst);
		}

		private void AssertAllDifferent<T>(List<T> lst)
		{
			for (var i = 0; i < lst.Count - 1; i++)
			{
				for (var j = i + 1; j < lst.Count; j++)
				{
					Assert.NotEqual(lst[i], lst[j]);	
				}
			}
		}
	}
}