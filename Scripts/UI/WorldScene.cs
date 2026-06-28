using Godot;
using System;

public partial class WorldScene : Node2D
{

	public CharacterBody2D Hero;

	public override void _Ready()
	{
		Hero = GetNode<CharacterBody2D>("Hero");

		if (GameManager.Instance.HeroPosition != Vector2.Zero)
		{
			Hero.Position = GameManager.Instance.HeroPosition;
		}
	}
}
