using Godot;
using System;

public partial class Game : Node2D
{
    public int Lives { get; set; } = 3;
    public int Score { get; set; } = 0;
    public Player Player { get; set; }
	[Export]
    public Hud Hud { get; set; }
    public CanvasLayer CanvasLayer { get; set; }
    [Export]
    public PackedScene GameOverScreen { get; set; }

    public AudioStreamPlayer2D EnemyHitSound { get; set; }
    public AudioStreamPlayer2D PlayerTakeDamageSound { get; set; }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		EnemyHitSound = GetNode<AudioStreamPlayer2D>("EnemyHitSound");
        PlayerTakeDamageSound = GetNode<AudioStreamPlayer2D>("PlayerTakeDamageSound");
        Player = GetNode<Player>("Player");
		CanvasLayer = GetNode<CanvasLayer>("UI");
        Hud.SetScoreLabel(Score);
        Hud.SetLivesLeft(Lives);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_death_zone_area_entered(Area2D area)
	{
		if(area is Enemy)
		{
			var enemy = area as Enemy;
			enemy.Die();
		}
	}

	public async void _on_player_took_damage()
	{
        PlayerTakeDamageSound.Play();
        Lives--;
		Hud.SetLivesLeft(Lives);
		if(Lives <= 0)
		{
            Player.Die();
			await ToSignal(GetTree().CreateTimer(1.5), "timeout");
			//GetTree().CreateTimer(10.5).;
			var gameOverScreen = GameOverScreen.Instantiate() as GameOverScreen;
			gameOverScreen.SetScore(Score);
            CanvasLayer.AddChild(gameOverScreen);
		}
	}

	public void _on_enemy_spawner_enemy_spawned(Enemy enemyInstance)
	{
		enemyInstance.EnemyHit += OnEnemyDied;

    }

	public void OnEnemyDied()
	{
        EnemyHitSound.Play();
        Score += 100;
        Hud.SetScoreLabel(Score);
    }
}
