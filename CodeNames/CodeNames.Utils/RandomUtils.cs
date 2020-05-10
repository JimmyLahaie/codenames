using System;
using System.Collections.Generic;
using CodeNames.Models.Interfaces;

namespace CodeNames.Utils
{
	public class RandomUtils : IRandomUtils
	{
		private static readonly Random Rand = new Random();

		public void RandomizeList<T>(List<T> lst)
		{
			for (var i = 0; i < lst.Count; i++)
			{
				var temp = lst[i];
				var swapIndex = Rand.Next(lst.Count);
				lst[i] = lst[swapIndex];
				lst[swapIndex] = temp;
			}
		}

		public string GenerateCode()
		{
			const string friendlyChar = "ABCEFGHJKLMNPRSTUVWXYZ23456789";
			var code = "";

			for (var i = 0; i < 8; i++) code += Convert.ToString(friendlyChar[Rand.Next(friendlyChar.Length)]);

			return code;
		}

		public T SingleValue<T>(params T[] values)
		{
			var randomIndex = Rand.Next(values.Length);
			return values[randomIndex];
		}
	}
}