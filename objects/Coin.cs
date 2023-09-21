using Godot;
using System;

public partial class Coin : Area3D
{
    private float _time = 0.0f;
    private bool _grabbed = false;

	private AudioPlayer AudioPlayer;

	public override void _Ready()
	{
		AudioPlayer = GetNode<AudioPlayer>("/root/AudioPlayer");
	}
    public void OnBodyEntered(Node body)
    {
        if (body.HasMethod("CollectCoin") && !_grabbed)
        {
            body.Call("CollectCoin");

            // Play sound
			AudioPlayer.Play("res://sounds/coin.ogg");

            GetNode<MeshInstance3D>("Mesh").QueueFree(); // Make invisible
            GetNode<CpuParticles3D>("Particles").Emitting = false; // Stop emitting stars
            _grabbed = true;
        }
    }

    public override void _Process(double delta)
    {
        RotateY(2 * (float)delta); // Rotation
        Position = new Vector3(Position.X, Position.Y + (Mathf.Cos(_time * 5) * 1) * (float)delta, Position.Z); // Sine movement

        _time += (float)delta;
    }
}
