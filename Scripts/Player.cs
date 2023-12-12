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

    Node2D spawnPoint = null;

    // ParticleTraits particleTraits = null;
    // DeathParticles deathParticles = null;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        playerSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        spawnPoint = GetNode<Node2D>("%SpawnPoint");
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
        if (body.IsInGroup("Traps"))
        {
            DeathTween();
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

    private void FlipPlayer()
    {
        if (Velocity.X < 0)
        {
            playerSprite.FlipH = true;
        }
        else if (Velocity.Y > 0)
        {
            playerSprite.FlipH = false;
        }
    }

    private async void DeathTween()
    {
        var tween = CreateTween();
        tween.TweenProperty(this, new NodePath("scale"), Vector2.Zero, 0.15f);
        await ToSignal(tween, Tween.SignalName.Finished);
        GlobalPosition = spawnPoint.GlobalPosition;
        await ToSignal(GetTree().CreateTimer(0.3f), SceneTreeTimer.SignalName.Timeout);
        //AudioManager.respawnSfx.Play();
        RespawnTween();
    }

    private async void RespawnTween()
    {
        var tween = CreateTween();
        tween.Stop();
        tween.Play();
        tween.TweenProperty(this, new NodePath("scale"), new Vector2(1f, 1f), 0.15f);
        await ToSignal(tween, Tween.SignalName.Finished);
    }

    private async void JumpTween()
    {
        var tween = CreateTween();
        tween.TweenProperty(this, new NodePath("scale"), new Vector2(0.7f, 1.4f), 0.1f);
        await ToSignal(tween, Tween.SignalName.Finished);
        /*
        TODO: For some reason in original GDScript there is only one tween used for both tween directions
              buy
        */
        var tween2 = CreateTween();
        tween2.TweenProperty(this, new NodePath("scale"), new Vector2(1.0f, 1.0f), 0.1f);
        await ToSignal(tween2, Tween.SignalName.Finished);
    }
}
