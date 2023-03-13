using Game.Bomber.Config;
using Game.Bomber.Controller;
using Game.Bomber.Events;
using Game.Bomber.Model;
using Gameplay.Weapons.Bombs;
using System;

namespace Game.Bomber
{
	public class BomberGameManager : IDisposable
	{
		private BomberGameModel _model;
		private BomberGameEvents _events;
		private BomberGameController _controller;

		public IBomberGameModel Model => _model;
		public IBomberGameEvents Events => _events;
		public BomberGameController Controller => _controller;

		public BomberGameConfig Config { get; private set; }

		public BomberGameManager(BomberGameConfig config)
		{
			Config = config;
			Config.BombSelector.Init();
			BombBase.SetMaxCollidersCount(Config.AgentsCount);

			_model = new BomberGameModel(config);
			_events = new BomberGameEvents();
			_controller = new BomberGameController(_model, _events);
			_controller.Run();
		}

		public void Dispose()
		{
			_controller.Dispose();
			_model.Dispose();
		}
	}
}