using Gameplay.AI.Agents;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Gameplay.AI.Controller
{
	public class AgentDecisionCenter : IAgentDecisionCenter, IDisposable
	{
		private readonly LinkedList<IAgent> _agents = new LinkedList<IAgent>();
		private readonly IAgentBrain _brain;
		private Coroutine _decisionRoutine;

		public AgentDecisionCenter(IAgentBrain brain)
		{
			_brain = brain;
		}

		public void RegisterAgent(IAgent agent)
		{
			_agents.AddLast(agent);
			if (_decisionRoutine == null)
			{
				_decisionRoutine = CoroutineSource.StartCoroutine(DecisionProcess());
			}
		}

		public void UnregisterAgent(IAgent agent)
		{
			_agents.Remove(agent);
		}

		private IEnumerator DecisionProcess()
		{
			WaitForSeconds wait = new WaitForSeconds(0.5f);
			while (_agents.Count > 0)
			{
				for (var node = _agents.First; node != null; node = node.Next)
				{
					var agent = node.Value;
					_brain.MakeDecision(agent);
					yield return wait;
				}
			}
			_decisionRoutine = null;
		}

		public void Dispose()
		{
			_agents.Clear();
			if (_decisionRoutine != null)
			{
				CoroutineSource.StopCoroutine(_decisionRoutine);
			}
		}
	}
}
