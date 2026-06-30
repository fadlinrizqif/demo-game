using Godot;
using System;

public partial class MainMenu : Control
{
	private TextureButton _newGameButton;
	private TextureButton _exitButton;

	public override void _Ready()
	{
		_newGameButton = GetNode<TextureButton>("VBoxContainer/Start");
		_exitButton = GetNode<TextureButton>("VBoxContainer/Quit");

		_newGameButton.Pressed += OnStartPressed;
		_exitButton.Pressed += OnQuitPressed;
	}

	private void OnStartPressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/World/WorldScene.tscn");
	}

	private void OnQuitPressed()
	{
		GetTree().Quit();
	}
}
