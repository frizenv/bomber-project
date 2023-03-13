using Pools;
using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Gameplay.Weapons
{
	public interface IWeaponSelector
	{
		int Count { get; }
		int SelectedIndex { get; }
		void SelectByIndex(int index);
		event Action<int> SelectedIndexChangedEvent;
	}

	public class WeaponSelector<T> : IWeaponSelector, IDisposable where T : MonoBehaviour, IPoolObject
	{
		[SerializeField] private T[] _weapons = default;

		private ObjectPool<T>[] _pools;

		public int Count => _weapons.Length;
		public int SelectedIndex { get; private set; }
		public T SelectedWeapon => _weapons[SelectedIndex];
		private ObjectPool<T> SelectedPool => _pools[SelectedIndex];

		public event Action<int> SelectedIndexChangedEvent;

		public void SelectByIndex(int index)
		{
			if (index < 0 || index >= Count)
				return;
			SelectedIndex = index;
			SelectedIndexChangedEvent?.Invoke(SelectedIndex);
		}

		public void Init()
		{
			_pools = new ObjectPool<T>[Count];
			for (int i = 0; i < _pools.Length; i++)
			{
				_pools[i] = new ObjectPool<T>(OnCreate, OnGet, OnRelease, Despawn);
			}
			SelectByIndex(0);
		}

		public T GetInstance()
		{
			var handle = SelectedPool.Get(out T component);
			component.BindReleaseHandle(handle);
			return component;
		}

		protected void ReleaseInstance(T component)
		{
			component.Release();
		}

		protected virtual void Despawn(T component)
		{
			UnityEngine.Object.Destroy(component.gameObject);
		}

		protected virtual void OnRelease(T component)
		{
			component.gameObject.SetActive(false);
		}

		protected virtual void OnGet(T component)
		{
			component.gameObject.SetActive(true);
		}

		protected virtual T OnCreate()
		{
			var instance = UnityEngine.Object.Instantiate(SelectedWeapon);
			return instance;
		}

		public void Dispose()
		{
			foreach (var pool in _pools)
			{
				pool.Dispose();
			}
		}
	}
}
