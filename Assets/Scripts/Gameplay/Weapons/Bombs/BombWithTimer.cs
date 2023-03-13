using System.Collections;
using UnityEngine;

namespace Gameplay.Weapons.Bombs
{
	public sealed class BombWithTimer : BombBase
	{
		[SerializeField] private float _timer = 3f;

		private Coroutine _detonationRoutine;

		private void OnCollisionEnter(Collision collision)
		{
			if (_detonationRoutine == null)
			{
				_detonationRoutine = StartCoroutine(DetonationRoutine());
			}
		}

		private void OnDisable()
		{
			if (_detonationRoutine != null)
				StopCoroutine(_detonationRoutine);
		}

		private IEnumerator DetonationRoutine()
		{
			yield return new WaitForSeconds(_timer);
			Explode();
			_detonationRoutine = null;
		}
	}
}
