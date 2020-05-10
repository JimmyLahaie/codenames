using System;

namespace CodeNames.Models.Exceptions
{
	public class GameAlreadyExistsException : Exception
	{
		public GameAlreadyExistsException(string gameKey, string path) : base(
			$"Game with key '{gameKey}' already exist in '{path}'")
		{
		}
	}
}