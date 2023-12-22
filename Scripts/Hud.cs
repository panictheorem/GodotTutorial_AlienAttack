using Godot;
using System;

public partial class Hud : Control
{
    [Export]
    public Label LivesLeft { get; set; }
    public Label Score { get; set; }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Score = GetNode<Label>("Score");

    }

    public void SetScoreLabel(int newScore)
    {
        Score.Text = "Score: " + newScore;
    }

    public void SetLivesLeft(int amount)
    {
        LivesLeft.Text = amount.ToString();
    }
}
