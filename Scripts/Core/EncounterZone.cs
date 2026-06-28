using Godot;
using System;

public partial class EncounterZone : Area2D
{
	[Export] public CharacterData EnemyData { get; set; }

	public override void _Ready()
	{
		if (GameManager.Instance.IsZoneDefeated(Name))
		{
			SetDeferred("monitoring", false);
			Visible = false;
			return;
		}

		BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body.Name == "Hero")
		{
			GameManager.Instance.HeroPosition = body.Position;

			GameManager.Instance.PendingEnemyData = EnemyData;
			GameManager.Instance.PreviosScene = "res://Scenes/World/WorldScene.tscn";
			GameManager.Instance.LastEncounterZone = Name;
			GD.Print($"Encounter an Enemy: {EnemyData.CharacterName}");
			CallDeferred(nameof(StartBattle));
		}
	}

	private void StartBattle()
	{

		GetTree().ChangeSceneToFile("res://Scenes/Battle/BattleScene.tscn");
	}


}
