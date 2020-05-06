using System;

namespace CodeNames.Models.Exceptions
{
	public class GameNotFoundException : Exception
	{
		public GameNotFoundException(string gameKey, string path) : base($"Game with key '{gameKey}' does not exist in '{path}'")
		{
			
		}
	}
}