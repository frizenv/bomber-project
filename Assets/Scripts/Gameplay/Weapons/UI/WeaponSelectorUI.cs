using Input;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Weapons.UI
{
	public sealed class WeaponSelectorUI : MonoBehaviour, IKeyboardNumberHandler
	{
		public class WeaponSelectorUIArgs
		{
			public IWeaponSelector WeaponSelector;
		}

		[SerializeField] private RectTransform _content = default;
		[SerializeField] private SelectableItem _itemPrefab = default;

		private List<SelectableItem> _items = new List<SelectableItem>();
		private IWeaponSelector _weaponSelector;
		private SelectableItem _selectedItem;

		private IWeaponSelector WeaponSelector
		{
			get => _weaponSelector;
			set
			{
				if (_weaponSelector != null)
				{
					_weaponSelector.SelectedIndexChangedEvent -= SelectedIndexChangedEventHandler;
				}
				_weaponSelector = value;
				if (_weaponSelector != null)
				{
					_weaponSelector.SelectedIndexChangedEvent += SelectedIndexChangedEventHandler;
				}
			}
		}

		public void Init(WeaponSelectorUIArgs args)
		{
			ClearItems();

			WeaponSelector = args.WeaponSelector;
			for (int i = 0; i < WeaponSelector.Count; i++)
			{
				SelectableItem instance = Instantiate(_itemPrefab, _content);
				instance.SelectedEvent += ItemSelectedEventHandler;
				instance.Index = i;
				_items.Add(instance);
			}
			SelectedIndexChangedEventHandler(WeaponSelector.SelectedIndex);
		}

		private void ClearItems()
		{
			_selectedItem = null;
			foreach (var item in _items)
			{
				item.SelectedEvent -= ItemSelectedEventHandler;
				Destroy(item.gameObject);
			}
			_items.Clear();
		}

		private void SelectedIndexChangedEventHandler(int index)
		{
			_selectedItem?.Deselect();
			_selectedItem = _items[index];
			_selectedItem.Select();
		}

		private void ItemSelectedEventHandler(SelectableItem item)
		{
			WeaponSelector.SelectByIndex(item.Index);
		}

		private void OnDestroy()
		{
			WeaponSelector = null;
		}

		void IKeyboardNumberHandler.HandleKeyboardNumber(int number)
		{
			WeaponSelector.SelectByIndex(number - 1);
		}
	}
}