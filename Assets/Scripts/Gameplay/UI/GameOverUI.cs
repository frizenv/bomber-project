using Game.Bomber.Events;
using UnityEngine;

namespace Gameplay.UI
{
	public class GameOverUI : MonoBehaviour
	{
		public class GameOverUIArgs
		{
			public IBomberGameEvents Events;
		}

		[SerializeField] private GameObject[] _enableOnGameOver = default;
		[SerializeField] private GameObject[] _disableOnGameOver = default;

		private IBomberGameEvents _events;

		public void Init(GameOverUIArgs args)
		{
			_events = args.Events;
			_events.GameFinishedEvent += GameFinishedEventHandler;
		}

		private void GameFinishedEventHandler()
		{
			_events.GameFinishedEvent -= GameFinishedEventHandler;
			foreach (var item in _enableOnGameOver)
			{
				item.SetActive(true);
			}
			foreach (var item in _disableOnGameOver)
			{
				item.SetActive(false);
			}
		}
	}
}