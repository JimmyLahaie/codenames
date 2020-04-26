using System.Collections.Generic;

namespace CodeNames.Interfaces
{
    public interface IWordsRepository
    {
        List<string> Get25RandomWords(bool easyWordOnly);
    }
}