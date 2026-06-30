using Godot;

public partial class GameOver : Control
{

	private TextureButton _retryButton;
	private TextureButton _exitButton;

	public override void _Ready()
	{
		_retryButton = GetNode<TextureButton>("VBoxContainer/Start");
		_exitButton = GetNode<TextureButton>("VBoxContainer/Quit");

		_retryButton.Pressed += OnStartPressed;
		_exitButton.Pressed += OnQuitPressed;
	}

	private void OnStartPressed()
	{
		GameManager.Instance.PreviosScene = "res://Scenes/GameOver.tscn";
		GetTree().ChangeSceneToFile("res://Scenes/World/WorldScene.tscn");
	}

	private void OnQuitPressed()
	{
		GetTree().Quit();
	}
}
