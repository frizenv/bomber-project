using System.Collections;
using UnityEngine;

namespace Utils
{
	public class CoroutineSource : MonoBehaviour
	{
		private static CoroutineSource _instance;

		private static CoroutineSource Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new GameObject("CoroutineSource").AddComponent<CoroutineSource>();
					DontDestroyOnLoad(_instance.gameObject);
				}

				return _instance;
			}
		}

		public new static Coroutine StartCoroutine(IEnumerator routine)
		{
			return ((MonoBehaviour)Instance).StartCoroutine(routine);
		}

		public new static void StopCoroutine(IEnumerator routine)
		{
			if (_instance != null)
				((MonoBehaviour)_instance).StopCoroutine(routine);
		}

		public new static void StopCoroutine(Coroutine routine)
		{
			if (_instance != null)
				((MonoBehaviour)_instance).StopCoroutine(routine);
		}

		private void OnDestroy()
		{
			_instance = null;
		}
	}
}