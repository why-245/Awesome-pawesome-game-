using Godot;
using System;

public partial class Bullet3d : Area3D
{
	public const float Speed = 25.0f;
	public const float range = 45.0f;
	public float TravelledDistance = 0.0f;
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Position += -Transform.Basis.Z * Speed * (float)delta;
		TravelledDistance += Speed * (float)delta;
		
		if (TravelledDistance > range)
		{
			this.QueueFree();
		}
	}
	
	private void _on_body_entered(Node3D body)
	{ 
		GD.Print("owie");
		if (body is Enemy enemy)
		{
			enemy.TakeDamage();
			GD.Print("Die!!!!!");
		}
		QueueFree();
	}
	
}
