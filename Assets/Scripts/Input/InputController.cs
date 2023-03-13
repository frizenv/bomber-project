using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
	public class InputController : MonoBehaviour
	{
		private InputActions _inputActions;
		private IClickHandler _clickHandler;
		private IKeyboardNumberHandler _keyboardNumberHandler;

		private void Awake()
		{
			_inputActions = new InputActions();
		}

		private void Start()
		{
			_inputActions.Default.Click.performed += ClickHandler;
			_inputActions.Default.KeyboardNumber.performed += KeyboardNumberPerformedEventHandler;
		}

		private void OnDestroy()
		{
			_inputActions.Default.Click.performed -= ClickHandler;
			_inputActions.Default.KeyboardNumber.performed -= KeyboardNumberPerformedEventHandler;
		}

		private void OnEnable()
		{
			_inputActions.Enable();
		}

		private void OnDisable()
		{
			_inputActions.Disable();
		}

		public void BindClickHandler(IClickHandler inputHandler)
		{
			_clickHandler = inputHandler;
		}

		public void BindKeyboardNumberHandler(IKeyboardNumberHandler keyboardNumberHandler)
		{
			_keyboardNumberHandler = keyboardNumberHandler;
		}

		private void ClickHandler(InputAction.CallbackContext ctx)
		{
			Vector2 position = _inputActions.Default.Position.ReadValue<Vector2>();
			_clickHandler.HandleClick(position);
		}

		private void KeyboardNumberPerformedEventHandler(InputAction.CallbackContext ctx)
		{
			int keyboardNumber = (int)ctx.ReadValue<float>();
			_keyboardNumberHandler.HandleKeyboardNumber(keyboardNumber);
		}

	}
}