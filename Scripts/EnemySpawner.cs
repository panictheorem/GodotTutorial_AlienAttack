using Godot;
using System;

public partial class EnemySpawner : Node2D
{
	[Signal]
    public delegate void EnemySpawnedEventHandler(Enemy enemyInstance);
    [Export]
    public PackedScene EnemyScene { get; set; }
    [Export]
    public PackedScene PathEnemyScene { get; set; }
    public Node2D SpawnPositions { get; set; }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		SpawnPositions = GetNode("SpawnPositions") as Node2D;
    }

	public void _on_timer_timeout()
	{
		SpawnEnemy();
    }

    public void _on_path_enemy_timer_timeout()
    {
        var enemyInstance = PathEnemyScene.Instantiate() as PathEnemy; ;
        AddChild(enemyInstance);
        GD.Print($"Path enemy: {enemyInstance.Enemy}");
        EmitSignal(SignalName.EnemySpawned, enemyInstance.Enemy);
    }

    public void SpawnEnemy()
	{
		var spawnPositions = SpawnPositions.GetChildren();
		var spawnPoint = spawnPositions.PickRandom() as Marker2D;
		var enemyInstance = EnemyScene.Instantiate() as Enemy;
		enemyInstance.GlobalPosition = spawnPoint.GlobalPosition;
		AddChild(enemyInstance);
		EmitSignal(SignalName.EnemySpawned, enemyInstance);
	}
}
