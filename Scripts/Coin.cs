using Godot;
using System;

public partial class Coin : Node
{
    int timePassed = 0;
    Vector2 initialPosition = Vector2.Zero;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GD.Print("C# Coin Ready");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        GD.Print("C# Coin Process");
    }
}
