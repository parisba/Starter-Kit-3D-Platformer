using Godot;
using System;

public partial class PlatformFalling : Node3D
{
    private bool _falling = false;
    private float _gravity = 0.0f;

    private AudioPlayer AudioPlayer;

    public override void _Ready()
	{
		AudioPlayer = GetNode<AudioPlayer>("/root/AudioPlayer");
	}

    public override void _Process(double delta)
    {
        Scale = Scale.Lerp(new Vector3(1, 1, 1), (float)delta * 10); // Animate scale

        Position = new Vector3(Position.X, Position.Y - _gravity * (float)delta, Position.Z);

        if (Position.Y < -10)
        {
            QueueFree(); // Remove platform if below threshold
        }

        if (_falling)
        {
            _gravity += 0.25f;
        }
    }

    public void OnBodyEntered(Node body)
    {
        if (!_falling)
        {
            AudioPlayer.Play("res://sounds/fall.ogg");
            Scale = new Vector3(1.25f, 1, 1.25f); // Animate scale
        }

        _falling = true;
    }
}
