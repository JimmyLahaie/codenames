using CodeNames.Models.DTO;
using Microsoft.AspNet.SignalR;

namespace CodeNames.WebApp.WebSockets
{
	public class GameHub : Hub
	{
		public void Send(string name, string message)
		{
			// Call the addNewMessageToPage method to update clients.
			Clients.All.addNewMessageToPage(name, message);
		}

		public void GiveHint(string gameKey, string word, int number, PlayerType player)
		{
			Clients.Group(gameKey).Hint(word, number, (int)player);
		}

		public void ChooseCard(string gameKey, int x, int y)
		{
			//var choiceInfo = _gameService.ValidateChoice(gameKey, x, y);

			Clients.Group(gameKey).AgentChoose(new ChooseCardResult
			{
				X = x,
				Y = y,
				CardColor = Color.Blue,
				TurnEnd = false
			});
		}

		public void AgentEndTurn(string gameKey)
		{
			Clients.Group(gameKey).NextPlayer("nextPlayerType", "nextPlayerColor");
		}

		public void JoinGame(string gameKey, string playerType)
		{
			Groups.Add(Context.ConnectionId, gameKey);
		}

		public void ExitGame(string gameKey)
		{
			Groups.Remove(Context.ConnectionId, gameKey);
		}
	}

	public class ChooseCardResult
	{
		public int X { get; set; }
		public int Y { get; set; }
		public Color CardColor { get; set; }
		public bool TurnEnd { get; set; }
	}
}