using Godot;

public partial class NPC : StaticBody2D
{
	[Export] public string TimelineName { get; set; } = "NpcVillager";

	private Area2D _interactArea;
	private Node dialog;
	private bool _heroNearby = false;
	private bool _isDialogOpen = false;

	public override void _Ready()
	{
		_interactArea = GetNode<Area2D>("Area2D");
		_interactArea.BodyEntered += OnBodyEntered;
		_interactArea.BodyExited += OnBodyExited;


		dialog = GetNode("/root/Dialogic");
		dialog.Connect("timeline_ended", new Callable(this, nameof(OnDialogEnded)));
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey keyEvent
			&& keyEvent.Pressed
			&& keyEvent.Keycode == Key.E
			&& _heroNearby
			&& !_isDialogOpen)
		{
			StartDialog();
		}
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body.Name == "Hero")
		{
			_heroNearby = true;
			GD.Print("Click E to interact");
		}

	}

	private void OnBodyExited(Node2D body)
	{
		if (body.Name == "Hero")
		{
			_heroNearby = false;
		}
	}

	private void StartDialog()
	{
		_isDialogOpen = true;
		dialog.Call("start", TimelineName);
	}

	private void OnDialogEnded()
	{
		_isDialogOpen = false;
	}


}
