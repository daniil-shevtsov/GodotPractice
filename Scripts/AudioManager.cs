using Godot;
using System;

public partial class AudioManager : Node
{
    AudioEffect jumpSfx = null;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        jumpSfx = GetNode<AudioEffect>("JumpSfx");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }
}
