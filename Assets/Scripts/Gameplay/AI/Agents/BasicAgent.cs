using Gameplay.Damage;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.AI.Agents
{
	public sealed class BasicAgent : MonoBehaviour, IAgent, IDestroyable
	{
		[SerializeField] private Damageable _damageable = default;
		[SerializeField] private NavMeshAgent _agent = default;

		public event Action<BasicAgent> DeathEvent;
		public event Action<IDestroyable> DestroyEvent;

		public bool SetDestination(Vector3 destination)
		{
			return _agent.SetDestination(destination);
		}

		private void OnEnable()
		{
			_damageable.DeathEvent += DeathEventHandler;
		}

		private void OnDisable()
		{
			_damageable.DeathEvent -= DeathEventHandler;
		}

		public void Init()
		{
			_damageable.ResetComponent();
		}

		private void DeathEventHandler(Damageable damageable)
		{
			DestroyEvent?.Invoke(this);
			DeathEvent?.Invoke(this);
		}
	}
}
