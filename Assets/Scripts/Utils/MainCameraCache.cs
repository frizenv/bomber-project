using UnityEngine;

namespace Utils
{
	public static class MainCameraCache
	{
		private static Camera _camera;

		public static Camera Camera
		{
			get
			{
				if (_camera == null)
					_camera = Camera.main;

				return _camera;
			}
		}
	}
}