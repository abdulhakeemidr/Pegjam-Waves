using UnityEngine;

public class Song
{
    public AudioClip Clip { get; }

    public int Bpm { get; }
    public float SecondsPerBeat { get; }

    public Song(string fileName, int bpm)
    {
        Clip = Resources.Load<AudioClip>(fileName);
        
        Bpm = bpm;
        SecondsPerBeat = 60f / bpm;
    }
}
