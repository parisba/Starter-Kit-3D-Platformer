using Godot;
using System;

public partial class PlayerView : Node3D
{
    [Export(PropertyHint.None, "Properties")]
    public Player PlayerTarget { get; set; }

    [Export(PropertyHint.Range, "Zoom")]
    public float ViewZoomMinimum = 16;

    [Export(PropertyHint.Range, "Zoom")]
    public float ViewZoomMaximum = 4;

    [Export(PropertyHint.Range, "Zoom")]
    public float ViewZoomSpeed = 10;

    [Export(PropertyHint.Range, "Rotation")]
    public float ViewRotationSpeed = 120;

    private Vector3 _camRotation;
    private float _zoomLevel = 10;
    private Camera3D _playerCam;

    public override void _Ready()
    {
        //PlayerTarget = GetNode<Player>("../Player");
        _playerCam = GetNode<Camera3D>("Camera");
        _camRotation = RotationDegrees; // Initial rotation
    }

    public override void _PhysicsProcess(double delta)
    {
        // Set position and rotation to targets
        Position = Position.Lerp(PlayerTarget.Position, (float)delta * 4);
        RotationDegrees = RotationDegrees.Lerp(_camRotation, (float)delta * 6);
        _playerCam.Position = _playerCam.Position.Lerp(new Vector3(0, 0, _zoomLevel), 8 * (float)delta);

        HandleInput(delta);
    }

    private void HandleInput(double delta)
    {
        // Rotation
        var input = new Vector3();

        input.Y = Input.GetActionStrength("camera_right") - Input.GetActionStrength("camera_left");
        input.X = Input.GetActionStrength("camera_down") - Input.GetActionStrength("camera_up");

        _camRotation += input.LimitLength(1.0f) * ViewRotationSpeed * (float)delta;
        _camRotation.X = Mathf.Clamp(_camRotation.X, -80, -10);

        // Zooming
        _zoomLevel += (Input.GetActionStrength("zoom_out") - Input.GetActionStrength("zoom_in")) * ViewZoomSpeed * (float)delta;
        _zoomLevel = Mathf.Clamp(_zoomLevel, ViewZoomMaximum, ViewZoomMinimum);
    }
}