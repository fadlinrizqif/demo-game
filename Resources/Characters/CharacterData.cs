using Godot;
using System;

[GlobalClass]
public partial class CharacterData : Resource
{
	[Export] public string CharacterName { get; set; } = "Unknown";
	[Export] public int MaxHP { get; set; } = 100;
	[Export] public int MaxMP { get; set; } = 50;
	[Export] public int Attack { get; set; } = 10;
	[Export] public int Defense { get; set; } = 5;
	[Export] public int Speed { get; set; } = 10;
	[Export] public Texture2D Sprite { get; set; }
}
