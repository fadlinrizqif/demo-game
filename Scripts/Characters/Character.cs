using Godot;

public partial class Character : Node
{
	[Export] public CharacterData Data { get; set; }

	public int CurrentHP { get; private set; }
	public int CurrentMP { get; private set; }

	public bool isDead => CurrentHP <= 0;

	private AnimatedSprite2D _sprite;

	public override void _Ready()
	{
		if (Data == null)
		{
			GD.PrintErr("Character: Data is null!");
		}

		CurrentHP = Data.MaxHP;
		CurrentMP = Data.MaxMP;

		_sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_sprite.Play("idle_down");

		GD.Print($"{Data.CharacterName} loaded! HP: {CurrentHP}/{Data.MaxHP}");

	}

	public int TakeDamage(int incomingAttack)
	{
		int damage = Mathf.Max(1, incomingAttack - Data.Defense);
		CurrentHP = Mathf.Max(0, CurrentHP - damage);
		return damage;
	}

	public int HealHP(int amount)
	{
		int healed = Mathf.Min(amount, Data.MaxHP - CurrentHP);
		CurrentHP += healed;
		return healed;
	}

	public bool UseMana(int amount)
	{
		if (CurrentMP < amount) return false;
		CurrentMP -= amount;
		return true;
	}
}
