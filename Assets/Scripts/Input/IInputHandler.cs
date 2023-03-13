using UnityEngine;

namespace Input
{
	public interface IClickHandler
	{
		void HandleClick(Vector2 screenPosition);
	}

	public interface IKeyboardNumberHandler
	{
		void HandleKeyboardNumber(int number);
	}
}
