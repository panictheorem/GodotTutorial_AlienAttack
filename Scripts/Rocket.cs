using Godot;
using System;

public partial class Rocket : Area2D
{
	[Export]
	public float Speed { get; set; } = 300.0f;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _PhysicsProcess(double delta)
    {
		GlobalPosition += new Vector2(Speed * (float)delta, 0);
    }

	public void _on_visible_on_screen_notifier_2d_screen_exited()
	{
        QueueFree();
	}

	public void _on_area_entered(Area2D area)
	{
		if(area is Enemy)
		{
			var enemy = (Enemy)area;
            enemy.Die();
			QueueFree();
		}
	}
}
