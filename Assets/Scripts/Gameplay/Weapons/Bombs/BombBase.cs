using Gameplay.Damage;
using Pools;
using System;
using UnityEngine;

namespace Gameplay.Weapons.Bombs
{
	public abstract class BombBase : MonoBehaviour, IPoolObject
	{
		[SerializeField] private float _range = 5f;
		[SerializeField] private float _damage = 50;
		[SerializeField] private LayerMask _layerMask;
		[SerializeField] private LayerMask _obstaclesLayerMask;

		public event Action<BombBase> ExplodeEvent;
		private IDisposable _releaseHandle;

		private static Collider[] _colliders = new Collider[10];

		public static void SetMaxCollidersCount(int count)
		{
			_colliders = new Collider[count];
		}

		protected void Explode()
		{
			int collisions = Physics.OverlapSphereNonAlloc(transform.position, _range, _colliders, _layerMask);
			for (int i = 0; i < collisions; i++)
			{
				if (_colliders[i].TryGetComponent<IDamageable>(out var target))
				{
					HandleInteraction(target);
				}
			}
			Array.Clear(_colliders, 0, collisions);
			ExplodeEvent?.Invoke(this);
		}

		protected virtual void HandleInteraction(IDamageable target)
		{
			Vector3 toTarget = target.transform.position - transform.position;
			if (Physics.Raycast(transform.position, toTarget, toTarget.magnitude, _obstaclesLayerMask))
			{
				return;
			}
			DamageMessage damage = new DamageMessage() { Damage = _damage };
			target.ApplyDamage(damage);
		}

		public void BindReleaseHandle(IDisposable releaseHandle)
		{
			_releaseHandle = releaseHandle;
		}

		public void Release()
		{
			_releaseHandle?.Dispose();
			_releaseHandle = null;
		}
	}
}
