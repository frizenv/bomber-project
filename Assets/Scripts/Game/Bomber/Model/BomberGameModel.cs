using Game.Bomber.Config;
using System;

namespace Game.Bomber.Model
{
	public class BomberGameModel : IBomberGameModel, IDisposable
    {
		public BomberGameConfig Config { get; private set; }
		public int AgentsCount { get; private set; }

		public BomberGameModel(BomberGameConfig config)
		{
			Config = config;
			AgentsCount = Config.AgentsCount;
		}

		public void SetAgentsCount(int agentsCount)
		{
			AgentsCount = agentsCount;
		}

		public void Dispose()
		{

		}
	}
}