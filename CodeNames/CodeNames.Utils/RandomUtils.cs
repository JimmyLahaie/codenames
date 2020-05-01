using System;
using System.Collections.Generic;
using CodeNames.Interfaces;

namespace CodeNames.Utils
{
	public class RandomUtils : IRandomUtils
	{
		public static Random _rand = new Random();

		public void RandomizeList<T>(List<T> lst)
		{
			for (int i = 0; i < lst.Count; i++)
			{
				var temp = lst[i];
				var swapIndex = _rand.Next(lst.Count);
				lst[i] = lst[swapIndex];
				lst[swapIndex] = temp;
			}
		}
		public string GenerateCode()
		{
			const string friendlyChar = "ABCEFGHJKLMNPRSTUVWXYZ23456789";
			var code = "";
            
			for (int i = 0; i < 8; i++)
			{
				code += Convert.ToString(friendlyChar[_rand.Next(friendlyChar.Length)]);
			}

			return code;
		}

		public T SingleValue<T>(params T[] values)
		{
			var randomIndex = _rand.Next(values.Length);
			return values[randomIndex];
		}
	}
}