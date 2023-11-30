using Godot;
using System;

public partial class GameManager : Node2D
{
    int score = 0;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() { }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }

    public void CoinCollected()
    {
        score += 1;
    }

    // func load_next_level(next_scene : PackedScene):
    // 		get_tree().change_scene_to_packed(next_scene)
}
