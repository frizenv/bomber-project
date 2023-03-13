using Gameplay.UI;
using Gameplay.Weapons.UI;
using Input;
using UnityEngine;

namespace Game.Bomber
{
	public class BomberGameUIManager : MonoBehaviour
	{
		[SerializeField] private WeaponSelectorUI _weaponSelector = default;
		[SerializeField] private AgentsLeftUI _agentsLeft = default;
		[SerializeField] private GameOverUI _gameOver = default;

		private BomberGameManager _gameManager;

		public IKeyboardNumberHandler KeyboardNumberHandler => _weaponSelector;

		public void Init(BomberGameManager gameManager)
		{
			_gameManager = gameManager;
			_weaponSelector.Init(new WeaponSelectorUI.WeaponSelectorUIArgs { WeaponSelector = _gameManager.Config.BombSelector });
			_agentsLeft.Init(new AgentsLeftUI.AgentsLeftUIArgs { Model = _gameManager.Model, Events = _gameManager.Events });
			_gameOver.Init(new GameOverUI.GameOverUIArgs { Events = _gameManager.Events });
		}
	}
}