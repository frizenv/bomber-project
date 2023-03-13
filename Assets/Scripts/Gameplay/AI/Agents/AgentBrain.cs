using Gameplay.AI.Navigation;

namespace Gameplay.AI.Agents
{
	public sealed class AgentBrain : IAgentBrain
	{
		private readonly IDestinationProvider _destinationProvider;

		public AgentBrain(IDestinationProvider destinationProvider)
		{
			_destinationProvider = destinationProvider;
		}

		public void MakeDecision(IAgent agent)
		{
			agent.SetDestination(_destinationProvider.GetDestination());
		}
	}
}