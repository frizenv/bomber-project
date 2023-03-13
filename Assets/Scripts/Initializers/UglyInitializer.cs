using Game.Bomber;
using Game.Bomber.Config;
using Gameplay.AI.Agents;
using Gameplay.AI.Controller;
using Gameplay.AI.Navigation;
using Gameplay.Spawners;
using Input;
using UnityEngine;

namespace Initializers
{
	[DefaultExecutionOrder(-1)]
	public class UglyInitializer : MonoBehaviour
	{
		[SerializeField] private BomberGameConfig _config = default;
		[SerializeField] private AgentsSpawner _agentSpawner = default;
		[SerializeField] private PointsGrid _grid = default;
		[SerializeField] private InputController _playerInput = default;
		[SerializeField] private BombSpawner _bombSpawner = default;
		[SerializeField] private BomberGameUIManager _bomberGameUIManager = default;

		private IAgentDecisionCenter _decisionCenter;
		private BomberGameManager _gameManager;

		private void Awake()
		{
			_gameManager = new BomberGameManager(_config);

			IAgentBrain brain = new AgentBrain(_grid);
			_decisionCenter = new AgentDecisionCenter(brain);
			_agentSpawner.Init(_grid, _decisionCenter, _gameManager.Controller.Tracker, _gameManager.Config);
			_agentSpawner.StartSpawning();

			_bombSpawner.Init(_gameManager.Config.BombSelector);

			_bomberGameUIManager.Init(_gameManager);
			_playerInput.BindClickHandler(_bombSpawner);
			_playerInput.BindKeyboardNumberHandler(_bomberGameUIManager.KeyboardNumberHandler);
		}

		private void OnDestroy()
		{
			_decisionCenter.Dispose();
			_agentSpawner.Dispose();
		}
	}
}