using UnityEngine;

public class Song
{
    public AudioClip Clip { get; }

    public int Bpm { get; }
    public float SecondsPerBeat { get; }

    public Song(AudioClip clip, int bpm)
    {
        Clip = clip;
        Bpm = bpm;
        SecondsPerBeat = 60f / bpm;
    }
}
