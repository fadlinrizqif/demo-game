using Godot;

public partial class BattleUI : CanvasLayer
{
	[Export] public BattleManager BattleManager { get; set; }

	//Hero UI
	private Sprite2D _heroImage;
	private Label _heroName;
	private TextureProgressBar _heroHP;
	private Label _heroHPCount;

	//Enemy UI
	private Sprite2D _enemyImage;
	private Label _enemyName;
	private TextureProgressBar _enemyHP;
	private Label _enemyHPCount;

	//Battle Log
	private Label _battleLog;

	// Action
	private MenuSelector _actionMenu;

	private Button _attackButton;
	private Button _healButton;

	public override void _Ready()
	{
		_heroImage = GetNode<Sprite2D>("Hero/Sprite2D");
		_heroName = GetNode<Label>("HeroInfo/HeroName");
		_heroHP = GetNode<TextureProgressBar>("HeroInfo/HBoxContainer/HeroHP");
		_heroHPCount = GetNode<Label>("HeroInfo/HBoxContainer/HeroHPCount");
		_enemyImage = GetNode<Sprite2D>("Enemy/Sprite2D");
		_enemyName = GetNode<Label>("EnemyInfo/EnemyName");
		_enemyHP = GetNode<TextureProgressBar>("EnemyInfo/HBoxContainer/EnemyHP");
		_enemyHPCount = GetNode<Label>("EnemyInfo/HBoxContainer/EnemyHPCount");
		_battleLog = GetNode<Label>("BattleLog");
		//_attackButton = GetNode<Button>("ActionButtons/AttackButton");
		//_healButton = GetNode<Button>("ActionButtons/HealButton");

		//_attackButton.Pressed += OnAttackPressed;
		//_healButton.Pressed += OnHealPressed;

		_actionMenu = GetNode<MenuSelector>("ActionMenu");
		_actionMenu.ItemSelected += OnMenuItemSelected;

		BattleManager.StateChanged += UpdateUI;
		UpdateUI();
	}

	private void OnMenuItemSelected(int index)
	{
		switch (index)
		{
			case 0: BattleManager.PlayerAttack(); break;
			case 1: BattleManager.PlayerHeal(); break;
			case 2: /*BattleManager.PlayerAttack()*/; break;
			case 3: /*BattleManager.PlayerAttack()*/; break;

		}
	}

	private void OnAttackPressed()
	{
		BattleManager.PlayerAttack();
		UpdateUI();
	}

	private void OnHealPressed()
	{
		BattleManager.PlayerHeal();
		UpdateUI();
	}

	public void UpdateUI()
	{
		var hero = BattleManager.Hero;
		var enemy = BattleManager.Enemy;

		//Update Hero
		_heroName.Text = hero.Data.CharacterName;
		_heroHP.MaxValue = hero.Data.MaxHP;
		_heroHP.Value = hero.CurrentHP;
		_heroHPCount.Text = $"{hero.CurrentHP}/{hero.Data.MaxHP}";

		//Update Ememy
		_enemyImage.Texture = enemy.Data.Sprite;
		_enemyName.Text = enemy.Data.CharacterName;
		_enemyHP.MaxValue = enemy.Data.MaxHP;
		_enemyHP.Value = enemy.CurrentHP;
		_enemyHPCount.Text = $"{enemy.CurrentHP}/{enemy.Data.MaxHP}";

		bool isPlayerTurn = BattleManager.CurrentState == BattleManager.BattleState.PlayerTurn;


		_actionMenu.SetEnabled(isPlayerTurn);


		if (BattleManager.CurrentState == BattleManager.BattleState.Won)
		{
			_battleLog.Text = "You Win";
			_actionMenu.SetEnabled(false);
		}
		else if (BattleManager.CurrentState == BattleManager.BattleState.Lost)
		{
			_battleLog.Text = "You Lost";
			_actionMenu.SetEnabled(isPlayerTurn);
		}


	}

	public void SetBattleLog(string message)
	{
		_battleLog.Text = message;
	}
}
