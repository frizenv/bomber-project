using System.Collections.Generic;
using UnityEngine.PlayerLoop;
using UnityEngine.LowLevel;
using UnityEngine;
using System;

namespace Optimization
{
	public enum UpdateType
	{
		Update,
		LateUpdate,
	}

	public static class UpdateManager
	{
		private static readonly List<Action> _update = new List<Action>(50);
		private static readonly List<Action> _lateUpdate = new List<Action>(50);
		private static bool _isInit = false;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void SetupUpdateManager()
		{
			if (_isInit) return;

			var playerLoop = PlayerLoop.GetCurrentPlayerLoop();
			for (int i = 0; i < playerLoop.subSystemList.Length; i++)
			{
				if (playerLoop.subSystemList[i].type == typeof(Update))
					playerLoop.subSystemList[i].updateDelegate += Update;
				if (playerLoop.subSystemList[i].type == typeof(PreLateUpdate))
					playerLoop.subSystemList[i].updateDelegate += LateUpdate;
			}
			PlayerLoop.SetPlayerLoop(playerLoop);
			_isInit = true;
		}

		public static void AddUpdateEvent(UpdateType updateType, Action updateAction)
		{
			if (updateAction == null) return;

			switch (updateType)
			{
				case UpdateType.Update:
					_update.Add(updateAction);
					break;
				case UpdateType.LateUpdate:
					_lateUpdate.Add(updateAction);
					break;
			}
		}

		public static void RemoveUpdateEvent(UpdateType updateEvent, Action updateAction)
		{
			if (updateAction == null) return;

			switch (updateEvent)
			{
				case UpdateType.Update:
					_update.Remove(updateAction);
					break;
				case UpdateType.LateUpdate:
					_lateUpdate.Remove(updateAction);
					break;
			}
		}

		private static void Update()
		{
			for (int i = 0; i < _update.Count; i++)
			{
				_update[i].Invoke();
			}
		}

		private static void LateUpdate()
		{
			for (int i = 0; i < _lateUpdate.Count; i++)
			{
				_lateUpdate[i].Invoke();
			}
		}
	}
}