using System;
using UnityEngine;

namespace Gameplay.Damage
{
	public class Damageable : MonoBehaviour, IDamageable
	{
		[SerializeField] private float _maxHp = 100;

		public float MaxHp => _maxHp;
		public float CurrentHp { get; private set; }

		public event Action<float> HpChangedEventHandler;
		public event Action<Damageable> DeathEvent;


		public void ApplyDamage(DamageMessage message)
		{
			CurrentHp -= message.Damage;
			if (CurrentHp <= 0)
			{
				CurrentHp = 0;
				HpChangedEventHandler?.Invoke(message.Damage);
				DeathEvent?.Invoke(this);
				return;
			}
			HpChangedEventHandler?.Invoke(message.Damage);
		}

		public void ResetComponent()
		{
			CurrentHp = _maxHp;
			HpChangedEventHandler?.Invoke(0);
		}
	}
}
