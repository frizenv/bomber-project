using Gameplay.Weapons.Bombs;
using UnityEngine;

namespace Game.Bomber.Config
{
	[CreateAssetMenu(menuName = "Config/BomberGameConfig")]
	public class BomberGameConfig : ScriptableObject, IAgentsCountProvider
	{
		[SerializeField] private int _agentsCount = 10;
		[SerializeField] private BombSelector _bombSelector = default;

		public int AgentsCount => _agentsCount;
		public BombSelector BombSelector => _bombSelector;
	}
}
