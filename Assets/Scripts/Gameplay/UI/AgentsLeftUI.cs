using Game.Bomber.Events;
using Game.Bomber.Model;
using TMPro;
using UnityEngine;

namespace Gameplay.UI
{
	public class AgentsLeftUI : MonoBehaviour
	{
		public class AgentsLeftUIArgs
		{
			public IBomberGameModel Model;
			public IBomberGameEvents Events;
		}

		[SerializeField] private TextMeshProUGUI _text = default;

		private IBomberGameModel _model;
		private IBomberGameEvents _events;

		public void Init(AgentsLeftUIArgs args)
		{
			_model = args.Model;
			_events = args.Events;
			_events.AgentsCountChangedEvent += AgentsCountChangedEventHandler;
			_events.GameFinishedEvent += GameFinishedEvent;

			AgentsCountChangedEventHandler();
		}

		private void GameFinishedEvent()
		{
			Deinit();
		}

		private void AgentsCountChangedEventHandler()
		{
			_text.text = $"Agents left: {_model.AgentsCount}";
		}

		public void Deinit()
		{
			_events.AgentsCountChangedEvent -= AgentsCountChangedEventHandler;
			_events.GameFinishedEvent -= GameFinishedEvent;
		}
	}
}