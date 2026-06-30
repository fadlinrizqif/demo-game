using Godot;
using System;

public partial class WorldScene : Node2D
{

	public CharacterBody2D Hero;

	public override void _Ready()
	{
		Hero = GetNode<CharacterBody2D>("Hero");

		if (GameManager.Instance.PreviosScene == "res://Scenes/GameOver.tscn"
			|| GameManager.Instance.PreviosScene == "res://Scenes/Winning.tscn")
		{

			Hero.Position = Vector2.Zero;

		}
		else if (GameManager.Instance.HeroPosition != Vector2.Zero)
		{
			Hero.Position = GameManager.Instance.HeroPosition;
		}
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey keyEvent && keyEvent.Pressed && keyEvent.Keycode == Key.Escape)
		{

			GameManager.Instance.HeroPosition = Hero.Position;
			GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");

		}
	}
}
