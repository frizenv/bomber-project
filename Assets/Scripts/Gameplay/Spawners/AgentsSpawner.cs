using Game.Bomber.Analytics;
using Game.Bomber.Config;
using Gameplay.AI.Agents;
using Gameplay.AI.Controller;
using Gameplay.AI.Navigation;
using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Gameplay.Spawners
{
	public sealed class AgentsSpawner : MonoBehaviour, IDisposable
	{
		[SerializeField] private BasicAgent _prefab = default;
		[SerializeField] private Transform _parent = default;

		private ObjectPool<BasicAgent> _agentsPool;
		private IEntityTracker _tracker;
		private IDestinationProvider _destinationProvider;
		private IAgentDecisionCenter _decisionCenter;
		private IAgentsCountProvider _agentsCountProvider;

		public void Init(IDestinationProvider destinationProvider, IAgentDecisionCenter agentDecisionCenter, IEntityTracker tracker, IAgentsCountProvider agentsCountProvider)
		{
			_destinationProvider = destinationProvider;
			_decisionCenter = agentDecisionCenter;
			_tracker = tracker;
			_agentsCountProvider = agentsCountProvider;
		}

		public void StartSpawning()
		{
			if (_agentsPool == null)
			{
				_agentsPool = new ObjectPool<BasicAgent>(OnCreate, OnGet, OnRelease, Despawn);
			}

			for (int i = 0; i < _agentsCountProvider.AgentsCount; i++)
			{
				_agentsPool.Get();
			}
		}

		private void Despawn(BasicAgent agent)
		{
			if (agent != null)
				Destroy(agent.gameObject);
		}

		private BasicAgent OnCreate()
		{
			var instance = Instantiate(_prefab, _parent);
			return instance;
		}

		private void OnGet(BasicAgent agent)
		{
			agent.transform.position = _destinationProvider.GetDestination();
			_decisionCenter.RegisterAgent(agent);
			_tracker.AddEnitty(agent);

			agent.DeathEvent += AgentDeathEventHandler;
			agent.Init();
			agent.gameObject.SetActive(true);
		}

		private void OnRelease(BasicAgent agent)
		{
			_decisionCenter.UnregisterAgent(agent);
			agent.DeathEvent -= AgentDeathEventHandler;
			agent.gameObject.SetActive(false);
		}

		private void AgentDeathEventHandler(BasicAgent agent)
		{
			_agentsPool.Release(agent);
		}

		public void Dispose()
		{
			_agentsPool.Dispose();
		}
	}
}