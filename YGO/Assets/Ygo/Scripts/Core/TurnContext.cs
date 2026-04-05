using System;
using System.Collections.Generic;

namespace Ygo.Core
{
    public class TurnContext
    {
        public IList<PlayerContext> Players { get; }
        public PlayerContext CurrentTurnPlayer => Players[_currentTurnPlayerIndex];
        public PlayerContext PointOfViewPlayer { get; private set; }
        
        public int CurrentTurn => _currentTurn;
        private int _currentTurn;
        
        private int _currentTurnPlayerIndex;

        public TurnContext(IList<PlayerContext> players)
        {
            if (players.Count < 2)
            {
                throw new InvalidOperationException("You must provide at least two players");
            }
            Players = players;
        }

        public void Init(int startingPlayerIndex, int startingPlayerHand)
        {
            _currentTurnPlayerIndex = startingPlayerIndex;
            _currentTurn = 1;

            foreach (var player in Players)
            {
                player.DrawFromDeck(startingPlayerHand);
            }

            SetPointOfView();
        }

        public void AdvanceTurn()
        {
            _currentTurn++;
            _currentTurnPlayerIndex++;
            if (_currentTurnPlayerIndex >= Players.Count)
                _currentTurnPlayerIndex = 0;
            
            SetPointOfView();
        }

        private void SetPointOfView()
        {
            if (Players.Count == 2)
            {
                if (CurrentTurnPlayer.ShowViewPoint)
                {
                    PointOfViewPlayer = CurrentTurnPlayer;
                    return;
                }

                foreach (var player in Players)
                {
                    if (!player.ShowViewPoint) continue;
                    PointOfViewPlayer = player;
                    return;
                }
            }
            else
            {
                throw new NotImplementedException("Massive multiplayer not implemented");
            }
        }
    }
}