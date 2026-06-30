using Godot;
using System;

public partial class Cutscene : Area2D
{
	[Export] public string TimelineName { get; set; }
	[Export] public Vector2 HeroTargetPosition { get; set; }
	[Export] public float MoveDuration { get; set; } = 2.0f;

	private bool _triggered;

	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body.Name != "Hero" || _triggered) return;
		_triggered = true;

		var heroMovement = body as HeroMovement;
		if (heroMovement == null) return;

		heroMovement.SetControlEnabled(false);

		var tween = CreateTween();
		tween.TweenProperty(body, "position", HeroTargetPosition, MoveDuration);
		tween.TweenCallback(Callable.From(() => StartDialog(heroMovement)));
	}

	private void StartDialog(HeroMovement heroMovement)
	{
		var dialog = GetNode("/root/Dialogic");
		dialog.Call("start", TimelineName);
		dialog.Connect("timeline_ended", Callable.From(() =>
		{
			heroMovement.SetControlEnabled(true);
		}),
		  (uint)ConnectFlags.OneShot);
	}
}
