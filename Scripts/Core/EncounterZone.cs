using Godot;
using System;

public partial class EncounterZone : Area2D
{
	[Export] public CharacterData EnemyData { get; set; }
	[Export] public bool IsBoss { get; set; } = false;

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
		if (body.Name != "Hero") return;
		if (GameManager.Instance.IsZoneOnCooldown(Name)) return;

		GameManager.Instance.HeroPosition = body.Position;

		GameManager.Instance.IsBoss = IsBoss;
		GameManager.Instance.PendingEnemyData = EnemyData;
		GameManager.Instance.PreviosScene = "res://Scenes/World/WorldScene.tscn";
		GameManager.Instance.LastEncounterZone = Name;
		GD.Print($"Encounter an Enemy: {EnemyData.CharacterName}");
		CallDeferred(nameof(StartBattle));
	}

	private void StartBattle()
	{

		GetTree().ChangeSceneToFile("res://Scenes/Battle/BattleScene.tscn");
	}


}
