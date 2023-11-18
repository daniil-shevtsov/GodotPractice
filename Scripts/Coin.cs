using Godot;
using System;
using static System.MathF;

public partial class Coin : Area2D
{
    [Export]
    int amplitude = 4;

    [Export]
    int frequency = 5;

    double timePassed = 0.0;
    Vector2 initialPosition = Vector2.Zero;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        initialPosition = Position;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        coinHover(delta);
    }

    public void coinHover(double delta)
    {
        timePassed += delta;
        var newY = initialPosition.Y + amplitude * Sin((float)(frequency * timePassed));
        Position = new Vector2(Position.X, newY);
    }
}
