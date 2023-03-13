using Gameplay.AI.Agents;
using System;

namespace Gameplay.AI.Controller
{
	public interface IAgentDecisionCenter : IDisposable
	{
		void RegisterAgent(IAgent agent);
		void UnregisterAgent(IAgent agent);
	}
}
