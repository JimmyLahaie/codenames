using System.Collections.Generic;
using CodeNames.Cores.Utils;
using Xunit;

namespace CodeNames.Cores.Tests.Utils
{
	public class RandomCodeGeneratorTests
	{
		[Fact]
		public void CanGetRandomStrings()
		{
			var rand = new RandomCodeGenerator();
			var lst = new List<string>();
			for (int i = 0; i < 30; i++)
			{
				lst.Add(rand.GetRandomGameCode());
			}
			
			//todo finish this test
			for (int i = 0; i < lst.Count - 1; i++)
			{
				for (int j = i + 1; j < lst.Count; j++)
				{
					Assert.NotEqual(lst[i], lst[j]);
				}
			}
		}
	}
}