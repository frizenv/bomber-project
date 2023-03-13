using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Weapons.UI
{
	public class SelectableItem : MonoBehaviour
	{
		[SerializeField] private Image _image = default;
		[SerializeField] private TextMeshProUGUI _text = default;
		[SerializeField] private Button _button = default;
		[SerializeField] private Color _selectedColor = Color.green;
		[SerializeField] private Color _defaultColor = Color.white;

		public event Action<SelectableItem> SelectedEvent;

		private int _index;

		private void Start()
		{
			_button.onClick.AddListener(ButtonClickedEventHandler);
		}

		private void OnDestroy()
		{
			_button.onClick.RemoveListener(ButtonClickedEventHandler);
		}

		private void ButtonClickedEventHandler()
		{
			SelectedEvent?.Invoke(this);
		}

		public int Index
		{
			get => _index;
			set
			{
				_index = value;
				_text.text = _index.ToString();
			}
		}

		public void Select()
		{
			_image.color = _selectedColor;
		}

		public void Deselect()
		{
			_image.color = _defaultColor;
		}
	}
}
