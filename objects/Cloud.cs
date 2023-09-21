using Godot;
using System;

public partial class Cloud : Node3D
{
    private float _time = 0.0f;
    private RandomNumberGenerator _randomNumber = new RandomNumberGenerator();

    private float _randomVelocity;
    private float _randomTime;

    public override void _Ready()
    {
        _randomVelocity = _randomNumber.RandfRange(0.1f, 2.0f);
        _randomTime = _randomNumber.RandfRange(0.1f, 2.0f);
    }

    public override void _Process(double delta)
    {
        Position = new Vector3(Position.X, Position.Y + (Mathf.Cos(_time * _randomTime) * _randomVelocity) * (float)delta, Position.Z); // Sine movement

        _time += (float)delta;
    }
}
