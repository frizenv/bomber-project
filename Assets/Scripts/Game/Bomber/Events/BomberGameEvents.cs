using System;

namespace Game.Bomber.Events
{
    public class BomberGameEvents : IBomberGameEvents
    {
        public event Action AgentsCountChangedEvent;
        public event Action GameFinishedEvent;

        public void AgentsCountChanged()
        {
            AgentsCountChangedEvent?.Invoke();
        }

        public void GameFinished()
        {
            GameFinishedEvent?.Invoke();
        }
    }
}