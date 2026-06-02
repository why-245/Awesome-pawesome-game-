using Godot;
using System;

public partial class Player : CharacterBody3D
{
	public const float Speed = 5.0f;

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		
		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Vector3 direction = new Vector3(inputDir.X, 0, inputDir.Y).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}
		Velocity = velocity;
		MoveAndSlide();
			
			
		if (Input.IsActionJustPressed("shoot")) 
		{
			
			look_at_mouse();
			shoot_bullet();
		}
	}
	
	
	void shoot_bullet()
	{
		var BULLET_3D = GD.Load<PackedScene>("uid://ouiq1b4hjbit");
		var new_bullet = BULLET_3D.Instantiate();
		AddChild(new_bullet);
	
		((Area3D)new_bullet).GlobalTransform = GetNode<Marker3D>("Marker3D").GlobalTransform;
	}
	
	private const float ray_length = 500.0f;
	
	private void look_at_mouse()
		{
			var target_plane = new Plane(Vector3.Up , Position.Y);
			var mouse_position = GetViewport().GetMousePosition();
			var camera = GetNode<Camera3D>("Camera3D");
			var From = camera.ProjectRayOrigin(mouse_position);
			var To = From + camera.ProjectRayNormal(mouse_position) * ray_length;
			var mouse_position_on_screen = target_plane.IntersectsRay(From, To);
			Vector3 w = (Vector3)mouse_position_on_screen;
			GetNode<Marker3D>("Marker3D").LookAt(w, Vector3.Up);
		}
}
