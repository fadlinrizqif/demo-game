using Godot;

public partial class BattleUI : CanvasLayer
{
	[Export] public BattleManager BattleManager { get; set; }

	//Hero UI
	private Label _heroName;
	private ProgressBar _heroHP;

	//Enemy UI
	private Label _enemyName;
	private ProgressBar _enemyHP;

	//Battle Log
	private Label _battleLog;

	// Action
	private Button _attackButton;
	private Button _healButton;

	public override void _Ready()
	{
		_heroName = GetNode<Label>("HeroInfo/HeroName");
		_heroHP = GetNode<ProgressBar>("HeroInfo/HeroHP");
		_enemyName = GetNode<Label>("EnemyInfo/EnemyName");
		_enemyHP = GetNode<ProgressBar>("EnemyInfo/EnemyHP");
		_battleLog = GetNode<Label>("BattleLog");
		_attackButton = GetNode<Button>("ActionButtons/AttackButton");
		_healButton = GetNode<Button>("ActionButtons/HealButton");

		_attackButton.Pressed += OnAttackPressed;
		_healButton.Pressed += OnHealPressed;

		UpdateUI();
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

		//Update Ememy
		_enemyName.Text = enemy.Data.CharacterName;
		_enemyHP.MaxValue = enemy.Data.MaxHP;
		_enemyHP.Value = enemy.CurrentHP;

		bool isPlayerTurn = BattleManager.CurrentState == BattleManager.BattleState.PlayerTurn;
		_attackButton.Disabled = !isPlayerTurn;
		_healButton.Disabled = !isPlayerTurn;


		if (BattleManager.CurrentState == BattleManager.BattleState.Won)
		{
			_battleLog.Text = "You Win";
			_attackButton.Disabled = true;
			_healButton.Disabled = true;
		}
		else if (BattleManager.CurrentState == BattleManager.BattleState.Lost)
		{
			_battleLog.Text = "You Lost";
			_attackButton.Disabled = true;
			_healButton.Disabled = true;
		}


	}

	public void SetBattleLog(string message)
	{
		_battleLog.Text = message;
	}
}
