using System.Collections.Generic;

namespace CodeNames.Models.Interfaces
{
	public interface IRandomUtils
	{
		void RandomizeList<T>(List<T> lst);
		string GenerateCode();
		T SingleValue<T>(params T[] values);
	}
}