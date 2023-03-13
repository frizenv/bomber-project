using Optimization;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Damage.UI
{
	public class HealthDisplay : MonoBehaviour
	{
		[SerializeField] private Image _progressBar = default;
		[SerializeField] private Damageable _damageable = default;
		[SerializeField] private float _zOffset = 1f;

		private void OnEnable()
		{
			UpdateManager.AddUpdateEvent(UpdateType.Update, AdjustPosition);
			_damageable.HpChangedEventHandler += HpChangedEventHandler;
			UpdateBar();
		}

		private void OnDisable()
		{
			UpdateManager.RemoveUpdateEvent(UpdateType.Update, AdjustPosition);
			_damageable.HpChangedEventHandler -= HpChangedEventHandler;
		}

		private void HpChangedEventHandler(float value)
		{
			UpdateBar();
		}

		private void UpdateBar()
		{
			_progressBar.fillAmount = _damageable.CurrentHp / _damageable.MaxHp;
		}

		private void AdjustPosition()
		{
			transform.rotation = Quaternion.Euler(90, 0, 0);
			Vector3 targetPosition = _damageable.transform.position;
			targetPosition.z += 1;
			transform.position = targetPosition;
		}
	}
}