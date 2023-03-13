using UnityEngine;

namespace Gameplay.AI.Navigation
{
	public interface IDestinationProvider
	{
		Vector3 GetDestination();
	}
}
