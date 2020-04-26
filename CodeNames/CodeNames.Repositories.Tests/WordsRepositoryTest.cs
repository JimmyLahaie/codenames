using System.Collections.Generic;
using Xunit;

namespace CodeNames.Repositories.Tests
{
    public class WordsRepositoryTest
    {
        [Fact]
        public void CanGetRandomWords()
        {
            //Yes, this is an empirical test
            var repo = new WordsRepository();
            var lstOfList = new List<List<string>>();
            
            for (int i = 0; i < 10; i++)
            {
                lstOfList.Add(repo.Get25RandomWords(false));
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = i+1; j < 10; j++)
                {
                    Assert.NotEqual(lstOfList[i], lstOfList[j]);
                }
            }
        }
    }
}