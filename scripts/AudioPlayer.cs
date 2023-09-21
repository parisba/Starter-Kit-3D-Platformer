using Godot;
using System;
using System.Collections.Generic;

public partial class AudioPlayer : Node
{
    private const int NumPlayers = 12;
    private const string Bus = "master";

    private List<AudioStreamPlayer> _available = new List<AudioStreamPlayer>();  // The available players.
    private Queue<string> _queue = new Queue<string>();  // The queue of sounds to play.
    
    public override void _Ready()
    {
        for (int i = 0; i < NumPlayers; i++)
        {
            var p = new AudioStreamPlayer();
            AddChild(p);

            _available.Add(p);

            p.VolumeDb = -10;

			//p.OnStreamFinished += () => { this.StreamIsFinished(p); };
            //p.Connect("finished", this, nameof(StreamIsFinished), new Godot.Collections.Array {p});
			//p.Finished.connect(StreamIsFinished.bind(p));
			p.Finished += () => { this.StreamIsFinished(p); };
            p.Bus = Bus;
        }
    }

    private void StreamIsFinished(AudioStreamPlayer stream)
    {
        _available.Add(stream);
    }

    public void Play(string soundPath)
    {
        _queue.Enqueue(soundPath);
    }

    public override void _Process(double delta)
    {
        if (_queue.Count > 0 && _available.Count > 0)
        {
            _available[0].Stream = (AudioStream)GD.Load(_queue.Dequeue());
            _available[0].Play();
			_available[0].PitchScale = 0.9f + GD.Randf() * (1.1f - 0.9f);

            _available.RemoveAt(0);
        }
    }
}
