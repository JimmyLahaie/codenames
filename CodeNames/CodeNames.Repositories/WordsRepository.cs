using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeNames.Interfaces;

namespace CodeNames.Repositories
{
    public class WordsRepository : IWordsRepository
    {
        private static readonly Random _rand = new Random();
        public List<string> Get25RandomWords(bool easyWordOnly)
        {
            var lst = _lst.Select(w => w).ToList();
            for (int i = 0; i < lst.Count; i++)
            {
                var temp = lst[i];
                var swapIndex = _rand.Next(lst.Count);
                lst[i] = lst[swapIndex];
                lst[swapIndex] = temp;
            }
            
            return lst.Take(25).Select(w => w.Word).ToList();
        }

        private List<WordData> _lst = new List<WordData>()
        {
            new WordData("soldat", true),
            new WordData("ballon", true),
            new WordData("nain", true),
            new WordData("mode", true),
            new WordData("neige", true),
            new WordData("don", true),
            new WordData("bar", true),
            new WordData("plan", true),
            new WordData("guide", true),
            new WordData("château", true),
            new WordData("appareil", true),
            new WordData("main", true),
            new WordData("jour", true),
            new WordData("hôpital", true),
            new WordData("pigeon", true),
            new WordData("cirque", true),
            new WordData("poingouin", true),
            new WordData("plume", true),
            new WordData("espion", true),
            new WordData("jet", true),
            new WordData("étude", true),
            new WordData("kiwi", true),
            new WordData("bête", true),
            new WordData("iris", true),
            new WordData("satellite", true),
            new WordData("cinéma", true),
            new WordData("flûte", true),
            new WordData("docteur", true),
            new WordData("voiture", true),
            new WordData("poisson", true)
        };
        
        private struct WordData
        {
            public string Word { get; set; }
            public bool IsSimpleWord { get; set; }

            public WordData(string word, bool isSimpleWord)
            {
                Word = word;
                IsSimpleWord = isSimpleWord;
            }
        }
    }
}