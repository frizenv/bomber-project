using Game.Bomber.Analytics;
using Game.Bomber.Events;
using Game.Bomber.Model;
using System;

namespace Game.Bomber.Controller
{
	public class BomberGameController : IDisposable
	{
		private BomberGameModel _model;
		private BomberGameEvents _events;
		private EntityCountCondition _countCondition;

		public bool IsRun { get; private set; }
		public IEntityTracker Tracker => _countCondition;

		public BomberGameController(BomberGameModel model, BomberGameEvents events)
		{
			_model = model;
			_events = events;
		}

		public void Run()
		{
			if (IsRun)
				return;
			IsRun = true;
			_countCondition = new EntityCountCondition();
			_countCondition.CountChangedEvent += CountChangedEventHandler;
		}

		public void Stop()
		{
			if (IsRun == false)
				return;
			IsRun = false;
			_countCondition.CountChangedEvent -= CountChangedEventHandler;
		}

		private void CountChangedEventHandler(int count)
		{
			_model.SetAgentsCount(count);
			_events.AgentsCountChanged();
			if (count == 0)
			{
				_events.GameFinished();
				Stop();
			}
		}

		public void Dispose()
		{
			_countCondition.Dispose();
		}
	}
}