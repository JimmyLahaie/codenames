using CodeNames.Cores.Services;
using CodeNames.Models.DTO;
using Microsoft.AspNet.SignalR;

namespace CodeNames.WebApp.WebSockets
{
	public class GameHub : Hub
	{
		private readonly IGameServices _gameServices;

		public GameHub(IGameServices gameServices) : base()
		{
			_gameServices = gameServices;
		}
		
		public void Send(string name, string message)
		{
			// Call the addNewMessageToPage method to update clients.
			Clients.All.addNewMessageToPage(name, message);
		}

		public void GiveHint(string gameKey, string word, int number, Color player)
		{
			_gameServices.NewHint(gameKey, word, number, player);
			Clients.Group(gameKey).Hint(word, number, (int)player);
		}

		public void ChooseCard(string gameKey, int x, int y, Color player)
		{
			var choiceInfo = _gameServices.NewChoice(gameKey, x, y, player);

			Clients.Group(gameKey).AgentChoose(new ChooseCardResult
			{
				X = choiceInfo.X,
				Y = choiceInfo.Y,
				CardColor = choiceInfo.CardColor,
				TurnEnd = choiceInfo.TurnEnd
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