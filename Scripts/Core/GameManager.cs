using Godot;
using System.Collections.Generic;

public partial class GameManager : Node
{
	public static GameManager Instance { get; private set; }

	public CharacterData PendingEnemyData { get; set; }

	public string PreviosScene { get; set; }

	public Vector2 HeroPosition { get; set; }

	public List<string> DefeatedZones { get; set; } = new();

	public string LastEncounterZone { get; set; }

	public override void _Ready()
	{
		Instance = this;
	}

	public bool IsZoneDefeated(string zoneName)
	{
		return DefeatedZones.Contains(zoneName);
	}

	public void MarkDefeatedZone(string zoneName)
	{
		if (!DefeatedZones.Contains(zoneName)) DefeatedZones.Add(zoneName);
	}
}
