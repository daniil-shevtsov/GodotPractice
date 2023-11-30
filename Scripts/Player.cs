using Godot;
using System;

public partial class Player : CharacterBody2D
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
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        Movement();
        PlayerAnimations();
        FlipPlayer();
    }

    public void onBodyEntered(Node2D body)
    {
        GD.Print("Player onBodyEntered");
        if (body.IsInGroup("Traps"))
        {
            GD.Print("Player collided with Trap");
        }
    }

    private void Movement()
    {
        if (!IsOnFloor())
        {
            Velocity = new Vector2(Velocity.X, Velocity.Y + gravity);
        }
        else if (IsOnFloor())
        {
            jumpCount = maxJumpCount;
        }

        HandleJumping();

        var inputAxis = Input.GetAxis("Left", "Right");
        Velocity = new Vector2(inputAxis * moveSpeed, Velocity.Y);
        MoveAndSlide();
    }

    private void HandleJumping()
    {
        if (Input.IsActionJustPressed("Jump"))
        {
            GD.Print("Player jump pressed");
            if (IsOnFloor() && !doubleJump)
            {
                Jump();
            }
            else if (doubleJump && jumpCount > 0)
            {
                Jump();
                jumpCount -= 1;
            }
        }
    }

    private void Jump()
    {
        JumpTween();
        //AudioManager.JumpSfx.Play();
        Velocity = new Vector2(Velocity.X, Velocity.Y - jumpForce);
    }

    private void PlayerAnimations()
    {
        if (IsOnFloor())
        {
            if (Mathf.Abs(Velocity.X) > 0)
            {
                //particleTraits.emitting = true;
                playerSprite.Play("Walk", 1.5f);
            }
            else
            {
                playerSprite.Play("Idle");
            }
        }
        else
        {
            playerSprite.Play("Jump");
        }
    }

    private void FlipPlayer() { }

    private void DeathTween() { }

    private void RespawnTween() { }

    private void JumpTween() { }
}
