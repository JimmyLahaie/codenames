using CodeNames.Models.DTO;
using CodeNames.Models.Interfaces;

namespace CodeNames.Cores.Services
{
	public interface IGameServices
	{
		string CreateGame();
		Game GetGame(string id);
		void NewHint(string gameKey, string word, int number, Color player);
		ChoiceResult NewChoice(string gameKey, int i, int i1, Color player);
	}

	public class GameServices : IGameServices
	{
		private readonly IGameBuilder _gameBuilder;
		private readonly IGameRepository _gameRepository;

		public GameServices(IGameBuilder gameBuilder, IGameRepository gameRepository)
		{
			_gameBuilder = gameBuilder;
			_gameRepository = gameRepository;
		}

		public string CreateGame()
		{
			var newGame = _gameBuilder.GetNewGame();
			_gameRepository.Create(newGame);
			return newGame.Key;
		}

		public Game GetGame(string gameKey)
		{
			var game = _gameRepository.GetGame(gameKey);
			return game;
		}

		public void NewHint(string gameKey, string word, int number, Color player)
		{
			_gameRepository.AddHint(gameKey, word, number, player);
		}

		public ChoiceResult NewChoice(string gameKey, int x, int y, Color player)
		{
			_gameRepository.AddChoice(gameKey, x, y, player);
			var game = _gameRepository.GetGame(gameKey);
			var card = game.Cards[x, y];
			//var gameChoices = _gameRepository.GetActions(gameKey);
			//var currentState = GetCurrentTurnState(game, gameChoices);

			return new ChoiceResult
			{
				CardColor = card.Color,
				TurnEnd = false,
				Winner = null,
				LostByAssassin = false,
				X = x,
				Y = y
			};
			

			//todo : Get game then card
			//if already turn : ...
			//if assassin : game end
			//if same color as player Good continu
			//  if nbrturn > nbrword+1 -> end turn
			//  if all color card reveiled -> end game
			//else end
		}

		private CurrentGameState GetCurrentTurnState(Game game, GameActions gameChoices)
		{
			var state = new CurrentGameState();

			foreach (var action in gameChoices.Actions)
			{
				if (action.ActionFromType == PlayerType.SpyMaster)
				{
					var hintAction = (HintAction) action;
					state.NbrCardSuggested = hintAction.NbrCard;
					state.CurrentTurnNbrSelections = 0;
					state.LastSelectionCardColor = null;
				}

				if (action.ActionFromType == PlayerType.Agent)
				{
					var cardAction = (ChooseCardAction) action;
					state.CurrentTurnNbrSelections++;
					var card = game.Cards[cardAction.X, cardAction.Y];
					if (card.Color == Color.Blue)
					{
						state.NbrBlueFound++;
					}
					else if (card.Color == Color.Red)
					{
						state.NbrRedFound++;
					}

					state.LastSelectionCardColor = card.Color;
				}
			}

			return state;
			//nbr red found
			//nbr blue found
			//currentTurnNbrSelection
			//NbrCardSuggested
			//lastSelectionColor
		}

		private class CurrentGameState
		{
			public int NbrRedFound { get; set; }
			public int NbrBlueFound { get; set; }
			public int CurrentTurnNbrSelections { get; set; }
			public int NbrCardSuggested { get; set; }
			public Color? LastSelectionCardColor { get; set; }

		}
	}
}