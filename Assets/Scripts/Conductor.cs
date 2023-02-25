using System;
using UnityEngine;
using UnityEngine.Events;

public class Conductor : MonoBehaviour
{
    public UnityEvent onBeat;
    
    private AudioSource _music;
    private Song _currentSong;
    private float _songPosition = 0;
    private float _dspStartTime = 0;
    
    public bool Playing { get => _music.isPlaying; }
    public float BeatPosition { get; private set; }
    public float BeatOffset { get; private set; }

    void Start()
    {
        _music = GetComponent<AudioSource>();
        if (!_music) throw new Exception("Conductor requires an AudioSource");

        _music.loop = true;
    }

    void Update()
    {
        if (_music.isPlaying)
        {
            // Determine where we are in the song time using the audio system's timing:
            // https://docs.unity3d.com/ScriptReference/AudioSettings-dspTime.html
            _songPosition = (float) AudioSettings.dspTime - _dspStartTime;

            BeatPosition = _songPosition / _currentSong.SecondsPerBeat;
            BeatOffset = BeatPosition - (float) Math.Round(BeatPosition);
            
            // Emit an "onBeat" message when we're on the beat ♪ 
            if (BeatPosition % 1 == 0)
            {
                onBeat.Invoke();
            }
        }
    }

    public void LoadSong(Song song)
    {
        _currentSong = song;
        _music.clip = _currentSong.Clip;
    }

    public void Play()
    {
        _music.Play();
        _dspStartTime = (float)AudioSettings.dspTime;
    }
}
