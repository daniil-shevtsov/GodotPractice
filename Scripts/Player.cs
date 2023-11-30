using Godot;
using System;

public partial class Player : Node
{
    [ExportCategory("Player Properties")]
    [Export]
    float moveSpeed = 400f;

    [Export]
    float jumpForce = 1600f;

    [Export]
    float gravity = 30f;

    [Export]
    int maxJumpCount = 2;
    int jumpCount = 2;

    [ExportCategory("Toggle Functions")]
    [Export]
    bool doubleJump = false;

    bool isGrounded = false;

    AnimatedSprite2D playerSprite = null;

    // SpawnPoint spawnPoint = null;
    // ParticleTraits particleTraits = null;
    // DeathParticles deathParticles = null;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        playerSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        playerSprite.Play("Walk", 1.5f);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }

    public void onBodyEntered(Node2D body)
    {
        GD.Print("Player onBodyEntered");
        if (body.IsInGroup("Traps"))
        {
            GD.Print("Player collided with Trap");
        }
    }

    private void Movement() { }

    private void HandleJumping() { }

    private void Jump() { }

    private void PlayerAnimations() { }

    private void FlipPlayer() { }

    private void DeathTween() { }

    private void RespawnTween() { }

    private void JumpTween() { }
}
