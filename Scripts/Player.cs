using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	public int SPEED = 10000;

	private AnimatedSprite2D animatedSprite2D;

	public override void _Ready()
	{
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 direction = GetMovementDirection();
		FlipSprite(direction.X);
		Move((float)delta, direction);
	}

	private void Move(float delta, Vector2 movement)
	{
		if (movement != Vector2.Zero)
		{
			Velocity = movement * SPEED * delta;
			animatedSprite2D.Play("Moving");
		}
		else
		{
			Velocity = Velocity.MoveToward(Vector2.Zero, SPEED * delta);
			animatedSprite2D.Play("Idle");
		}

		MoveAndSlide();
	}

	private void FlipSprite(float horizontalMovement)
	{
		if (horizontalMovement < 0)
		{
			animatedSprite2D.FlipH = true;
		}
		else if (horizontalMovement > 0)
		{
			animatedSprite2D.FlipH = false;
		}
	}

	private Vector2 GetMovementDirection()
	{
		return Input.GetVector("left_axis", "right_axis", "top_axis", "down_axis");
	}

	public Godot.Collections.Dictionary<string, Variant> Save()
	{
		return new Godot.Collections.Dictionary<string, Variant>()
		{
			{ "Filename", SceneFilePath },
			{ "Parent", GetTree().Root.GetPathTo(GetParent())}, //GetParent().Name },
			{ "PosX", Position.X },
			{ "PosY", Position.Y }
		};
	}
}
