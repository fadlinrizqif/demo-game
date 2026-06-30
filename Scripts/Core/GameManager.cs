using Godot;
using System.Collections.Generic;

public partial class GameManager : Node
{
	public static GameManager Instance { get; private set; }

	public CharacterData PendingEnemyData { get; set; }

	public bool IsBoss { get; set; }

	public string PreviosScene { get; set; }

	public Vector2 HeroPosition { get; set; }

	public List<string> DefeatedZones { get; set; } = new();

	public string LastEncounterZone { get; set; }

	private Dictionary<string, float> _zoneCooldowns = new();

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

	public void ResetStateGame()
	{
		GameManager.Instance.PendingEnemyData = null;
		GameManager.Instance.PreviosScene = "";
		GameManager.Instance.DefeatedZones = new();
		GameManager.Instance.LastEncounterZone = "";
	}

	public void TempDisableZone(string zoneName, float duration)
	{
		_zoneCooldowns[zoneName] = duration;
	}

	public bool IsZoneOnCooldown(string zoneName)
	{
		return _zoneCooldowns.ContainsKey(zoneName) && _zoneCooldowns[zoneName] > 0;
	}

	public override void _Process(double delta)
	{
		var keys = new List<string>(_zoneCooldowns.Keys);
		foreach (var key in keys)
		{
			_zoneCooldowns[key] -= (float)delta;
			if (_zoneCooldowns[key] <= 0)
			{
				_zoneCooldowns.Remove(key);
			}
		}
	}
}
