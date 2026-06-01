using Godot;
using System;

public partial class Bullet3d : Area3D
{
	public const float Speed = 25.0f;
	public const float range = 45.0f;
	
	public float travelled_distance = 0.0f;
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Position += -Transform.Basis.Z * Speed * (float)delta;
		travelled_distance += Speed * (float)delta;
		
		if (travelled_distance > range)
		{
			this.QueueFree();
		}
	
	
	}
}
