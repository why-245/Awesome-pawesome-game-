using Godot;
using System;

public partial class Player : CharacterBody3D
{
	public const float Speed = 5.0f;
	private const float ray_length = 500.0f;
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
	
		((Area3D)new_bullet).GlobalTransform = GetNode<Marker3D>("Node3D/Marker3D").GlobalTransform;
	}
	
	private void look_at_mouse()
		{
			var TatgetPlane = new Plane(Vector3.Up , Position.Y);
			var MousePosition = GetViewport().GetMousePosition();
			var Camera = GetNode<Camera3D>("Camera3D");
			var From = Camera.ProjectRayOrigin(MousePosition);
			var To = From + Camera.ProjectRayNormal(MousePosition) * ray_length;
			var mouse_position_on_screen = TatgetPlane.IntersectsRay(From, To);
			Vector3 w = (Vector3)mouse_position_on_screen;
			GetNode<Node3D>("Node3D").LookAt(w, Vector3.Up);
		}
}
