using Godot;

public partial class HeroMovement : CharacterBody2D
{
	[Export] public float Speed { get; set; } = 100f;
	[Export] public int StepBeforeEncounter { get; set; } = 10;

	private AnimatedSprite2D _sprite;
	private int _stepCount;
	private Vector2 _lastPosition;

	private bool _controlEnabled = true;

	public override void _Ready()
	{
		_sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_lastPosition = Position;
	}

	public void SetControlEnabled(bool enable)
	{
		_controlEnabled = enable;
		if (!enable)
		{
			Velocity = Vector2.Zero;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		if (!_controlEnabled) return;

		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

		if (direction != Vector2.Zero)
		{
			Velocity = direction.Normalized() * Speed;
			UpdateAnimation(direction);
		}
		else
		{
			Velocity = Vector2.Zero;
			_sprite.Play("idle_down");
		}

		MoveAndSlide();
		//CountStep();
	}

	private void UpdateAnimation(Vector2 direction)
	{
		if (direction.Y > 0) _sprite.Play("walk_down");
		else if (direction.Y < 0) _sprite.Play("walk_up");
		else if (direction.X > 0) _sprite.Play("walk_right");
		else if (direction.X < 0) _sprite.Play("walk_left");
	}

	private void CountStep()
	{
		_stepCount++;
		_lastPosition = Position;

		if (_stepCount >= StepBeforeEncounter)
		{
			_stepCount = 0;
			TriggerEncounter();
		}
	}

	private void TriggerEncounter()
	{
		GD.Print("Encounter an Enemy!");
	}
}
