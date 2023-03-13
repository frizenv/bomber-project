using System;

namespace Gameplay.Damage
{
	public interface IDestroyable
	{
		event Action<IDestroyable> DestroyEvent;
	}
}
