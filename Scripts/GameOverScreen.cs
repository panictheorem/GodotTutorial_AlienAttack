using Godot;
using System;

public partial class GameOverScreen : Control
{
	[Export]
    public Label Score { get; set; }
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public void SetScore(int newScore)
	{
		Score.Text = "SCORE: " + newScore;
	}

	public void _on_retry_button_pressed()
	{
		GetTree().ReloadCurrentScene();
	}
}
