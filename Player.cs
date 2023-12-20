using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 500.0f;
    public PackedScene RocketScene { get; set; }
    public Node RocketContainer { get; set; }
    public override void _Ready()
    {
        RocketScene = (PackedScene)GD.Load("res://Scenes/Rocket.tscn");
        RocketContainer = GetNode("RocketContainer");
        GD.Print(RocketContainer.Name + " stuff");
    }


    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("shoot"))
        {
            Shoot();
        }
    }
    // Get the gravity from the project settings to be synced with RigidBody nodes.
    //public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    public override void _PhysicsProcess(double delta)
	{
        Velocity = Vector2.Zero;

        if (Input.IsActionPressed("move_right"))
		{
			Velocity += new Vector2(1.0f * Speed, 0);
        }

        if (Input.IsActionPressed("move_left"))
        {
            Velocity += new Vector2(-1.0f * Speed, 0);
        }

        if (Input.IsActionPressed("move_up"))
        {
            Velocity += new Vector2(0, -1.0f * Speed);
        }

        if (Input.IsActionPressed("move_down"))
        {
            Velocity += new Vector2(0, 1.0f * Speed);
        }

        MoveAndSlide();

        var screenSize = GetViewportRect().Size;
        GlobalPosition = GlobalPosition.Clamp(Vector2.Zero, new Vector2(screenSize.X, screenSize.Y));
	}

    private void Shoot()
    {
        var rocketInstance = (Area2D)RocketScene.Instantiate();
        RocketContainer.AddChild(rocketInstance);
        rocketInstance.GlobalPosition = GlobalPosition;
        rocketInstance.GlobalPosition += new Vector2(65, 0);
    }
}
