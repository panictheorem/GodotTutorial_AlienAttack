using Godot;
using System;

public partial class PathEnemy : Path2D
{
	[Export]
    public PathFollow2D PathFollow2D { get; set; }
    public Enemy Enemy { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        PathFollow2D.ProgressRatio = 1;
        Enemy = GetNode<Enemy>("PathFollow2D/Enemy");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        PathFollow2D.ProgressRatio -= 0.25f * (float)delta;
        if(PathFollow2D.ProgressRatio <= 0)
        {
            QueueFree();
        }
    }
}
