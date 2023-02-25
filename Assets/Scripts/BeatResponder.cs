using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BeatResponder : MonoBehaviour
{
    private Conductor _conductor;
    void Start()
    {
        _conductor = FindObjectOfType<Conductor>();

        if (_conductor)
        {
            _conductor.onBeat.AddListener(OnBeat);
        }
    }

    public abstract void OnBeat();
}