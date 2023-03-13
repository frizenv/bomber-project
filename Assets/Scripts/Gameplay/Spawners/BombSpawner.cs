using Gameplay.Weapons.Bombs;
using Input;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace Gameplay.Spawners
{
	public class BombSpawner : MonoBehaviour, IClickHandler
	{
		[SerializeField] private BombBase _prefab = default;
		[SerializeField] private LayerMask _raycastLayerMask;

		private BombSelector _selector;

		public void Init(BombSelector selector)
		{
			_selector = selector;
		}

		public void HandleClick(Vector2 screenPosition)
		{
			if (EventSystem.current.IsPointerOverGameObject())
				return;
			Ray ray = MainCameraCache.Camera.ScreenPointToRay(screenPosition);
			if (Physics.Raycast(ray, out var hit, float.PositiveInfinity, _raycastLayerMask))
			{
				Spawn(hit.point);
			}
		}

		private void Spawn(Vector3 position)
		{
			position.y = transform.position.y;
			var bomb = _selector.GetInstance();
			bomb.transform.position = position;
		}
	}
}