using UnityEngine;

namespace Gameplay.Damage
{
	public interface IDamageable
	{
		void ApplyDamage(DamageMessage message);
		Transform transform { get; }
	}
}