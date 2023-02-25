using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Conductor : MonoBehaviour
{
    public UnityEvent onBeat;
    
    private AudioSource _music;
    private Song _currentSong;
    private float _songPosition = 0;
    private float _dspStartTime = 0;
    private int _prevBeat = 0;
    
    [SerializeField]
    public List<AudioClip> Clips = new List<AudioClip>(); // FIXME: Need a better way to manage tracks for levels
    
    public bool Playing { get => _music.isPlaying; }
    public float BeatPosition { get; private set; }
    public int CurrBeat { get; private set; }
    public float BeatOffset { get; private set; }
    public 

    void Start()
    {
        _music = GetComponent<AudioSource>();
        _music.loop = true;
        LoadSong(new Song(Clips[0], 120)); // FIXME: Related to above, assumes Clips[0] exists and is 120...
        Play();
    }

    void Update()
    {
        if (_music.isPlaying)
        {
            // Determine where we are in the song time using the audio system's timing:
            // https://docs.unity3d.com/ScriptReference/AudioSettings-dspTime.html
            _songPosition = (float) AudioSettings.dspTime - _dspStartTime;

            BeatPosition = _songPosition / _currentSong.SecondsPerBeat;
            CurrBeat = (int) Math.Round(BeatPosition);
            BeatOffset = BeatPosition - CurrBeat;
            
            // Emit an "onBeat" message when we're on the beat ♪ 
            if (BeatOffset > 0 && _prevBeat != CurrBeat)
            {
                _prevBeat = CurrBeat;
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
        _dspStartTime = (float) AudioSettings.dspTime;
        Debug.Log("START: " + _dspStartTime);
    }
}
