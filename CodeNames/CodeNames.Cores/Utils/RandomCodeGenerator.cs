using System;

namespace CodeNames.Cores.Utils
{
    public interface IRandomCodeGenerator
    {
        string GetRandomGameCode();
    }

    public class RandomCodeGenerator : IRandomCodeGenerator
    {
        private static Random _rand = new Random();
        private const string FriendlyChar = "ABCEFGHJKLMNPRSTUVWXYZ23456789";
        
        public string GetRandomGameCode()
        {
            var code = "";
            
            for (int i = 0; i < 8; i++)
            {
                code += Convert.ToString(FriendlyChar[_rand.Next(FriendlyChar.Length)]);
            }

            return code;
        }
    }
}