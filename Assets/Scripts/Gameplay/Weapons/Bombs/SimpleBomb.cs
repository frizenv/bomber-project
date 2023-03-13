using UnityEngine;

namespace Gameplay.Weapons.Bombs
{
	public sealed class SimpleBomb : BombBase
	{
		private void OnCollisionEnter(Collision collision)
		{
			Explode();
		}
	}
}