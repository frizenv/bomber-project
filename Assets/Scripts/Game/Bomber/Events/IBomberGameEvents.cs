using System;

namespace Game.Bomber.Events
{
	public interface IBomberGameEvents
	{
		event Action AgentsCountChangedEvent;
		event Action GameFinishedEvent;
	}
}
