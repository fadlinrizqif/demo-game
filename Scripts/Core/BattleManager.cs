using Godot;
using System;
using System.Collections.Generic;

public partial class BattleManager : Node
{
	[Export] public Character Hero { get; set; }
	[Export] public Character Enemy { get; set; }

	[Signal] public delegate void StateChangedEventHandler();

	public enum BattleState
	{
		PlayerTurn,
		EnemyTurn,
		Won,
		Lost
	}

	public BattleState CurrentState { get; private set; }

	public override void _Ready()
	{
		CurrentState = BattleState.PlayerTurn;
		GD.Print("=== Battle Start ===");
		GD.Print($"{Hero.Data.CharacterName} HP: {Hero.CurrentHP} / {Hero.Data.MaxHP}");
		GD.Print($"{Enemy.Data.CharacterName} HP: {Enemy.CurrentHP} / {Enemy.Data.MaxHP}");
		GD.Print("== Waiting player input - Player Turn");
		EmitSignal(SignalName.StateChanged);
	}

	public void PlayerAttack()
	{
		GD.Print("=== Player Turn ===");
		int damage = Enemy.TakeDamage(Hero.Data.Attack);
		GD.Print($"Hero attack! Enemy takes {damage} damage!");
		GD.Print($"Enemy HP: {Enemy.CurrentHP} / {Enemy.Data.MaxHP}");

		if (Enemy.isDead)
		{
			CurrentState = BattleState.Won;
			GD.Print("Enemy defeated! You win!");
			EmitSignal(SignalName.StateChanged);
			return;
		}

		CurrentState = BattleState.EnemyTurn;
		EmitSignal(SignalName.StateChanged);
		EnemyTurn();
	}

	public async void EnemyTurn()
	{
		GD.Print("=== Enemy Turn ===");

		await ToSignal(GetTree().CreateTimer(1.0f), Timer.SignalName.Timeout);

		int damage = Hero.TakeDamage(Enemy.Data.Attack);
		GD.Print($"Enemy attack! {Hero.Data.CharacterName} takes {damage} damage!");
		GD.Print($"{Hero.Data.CharacterName} HP: {Hero.CurrentHP} / {Hero.Data.MaxHP}");

		if (Hero.isDead)
		{
			CurrentState = BattleState.Lost;
			GD.Print("Game Over");
			EmitSignal(SignalName.StateChanged);
			return;
		}

		CurrentState = BattleState.PlayerTurn;
		GD.Print("== Waiting player input - Player Turn");
		EmitSignal(SignalName.StateChanged);

	}

	public void PlayerHeal()
	{
		if (CurrentState != BattleState.PlayerTurn) return;

		int healed = Hero.HealHP(30);
		GD.Print($"{Hero.Data.CharacterName} heals {healed} HP!");
		GD.Print($"{Hero.Data.CharacterName} HP: {Hero.CurrentHP} / {Hero.Data.MaxHP}");

		CurrentState = BattleState.EnemyTurn;
		EmitSignal(SignalName.StateChanged);
		EnemyTurn();
	}
}
