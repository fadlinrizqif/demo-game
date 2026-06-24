using Godot;
using System.Collections.Generic;

public partial class MenuSelector : PanelContainer
{
	[Signal] public delegate void ItemSelectedEventHandler(int index);

	private int _currentIndex = 0;
	private List<HBoxContainer> _menuItem = new();
	private List<TextureRect> _arrow = new();

	public override void _Ready()
	{
		var vbox = GetNode<VBoxContainer>("MarginContainer/VBoxContainer");

		foreach (var child in vbox.GetChildren())
		{
			if (child is HBoxContainer item)
			{
				_menuItem.Add(item);
				var arrow = item.GetNode<TextureRect>("Arrow");
				_arrow.Add(arrow);

				var index = _menuItem.Count - 1;
				item.MouseEntered += () => OnMouseEntered(index);
			}
		}

		UpdateArrow();
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey keyEvent && keyEvent.Pressed)
		{
			if (keyEvent.Keycode == Key.Down || keyEvent.Keycode == Key.S)
			{
				_currentIndex = (_currentIndex + 1) % _menuItem.Count;
				UpdateArrow();
			}
			else if (keyEvent.Keycode == Key.Up || keyEvent.Keycode == Key.W)
			{
				_currentIndex = (_currentIndex - 1 + _menuItem.Count) % _menuItem.Count;
				UpdateArrow();
			}
			else if (keyEvent.Keycode == Key.Enter || keyEvent.Keycode == Key.Space)
			{
				EmitSignal(SignalName.ItemSelected, _currentIndex);
			}
		}
	}

	private void OnMouseEntered(int index)
	{
		_currentIndex = index;
		UpdateArrow();
	}

	public void OnMenuItemClicked(int index)
	{
		_currentIndex = index;
		UpdateArrow();
		EmitSignal(SignalName.ItemSelected, index);
	}

	private void UpdateArrow()
	{
		for (int i = 0; i < _arrow.Count; i++)
		{
			_arrow[i].Visible = i == _currentIndex;
		}
	}

	public void SetEnabled(bool enabled)
	{
		SetProcessInput(enabled);
		Modulate = enabled ? new Color(1, 1, 1) : new Color(0.5f, 0.5f, 0.5f);
	}
}
