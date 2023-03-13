using Gameplay.Damage;
using System;
using System.Collections.Generic;

namespace Game.Bomber.Analytics
{
	public class EntityCountCondition : IEntityTracker, IDisposable
	{
		private HashSet<IDestroyable> _entities = new HashSet<IDestroyable>();

		public event Action<int> CountChangedEvent;

		public void AddEnitty(IDestroyable entity)
		{
			if (_entities.Add(entity))
			{
				entity.DestroyEvent += DestroyEventHandler;
				CountChangedEvent?.Invoke(_entities.Count);
			}
		}

		private void DestroyEventHandler(IDestroyable entity)
		{
			if (_entities.Remove(entity))
			{
				entity.DestroyEvent -= DestroyEventHandler;
				CountChangedEvent?.Invoke(_entities.Count);
			}
		}

		public void Dispose()
		{
			_entities.Clear();
			CountChangedEvent = null;
		}

	}
}
