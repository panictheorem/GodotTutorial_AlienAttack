using Godot;
using System;

public partial class Enemy : Area2D
{
	[Signal]
	public delegate void EnemyHitEventHandler();
	[Export]
	public float Speed { get; set; } = 300.0f;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		GlobalPosition += new Vector2(-Speed * (float)delta, 0);
	}

	public void Die(bool hitPlayer = false)
	{
		if(!hitPlayer)
		{
            EmitSignal(SignalName.EnemyHit);
        }
        QueueFree();
	}

	public void _on_visible_on_screen_notifier_2d_screen_exited()
	{
		QueueFree();
    }

	public void _on_body_entered(Node2D body)
	{
		if(body is Player)
		{
			var player = (Player)body;
			player.TakeDamage();
			Die(true);
        }
    }
}
