using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BeatResponder : MonoBehaviour
{
    [SerializeField]
    protected Conductor _conductor;
    private bool _isInitialized = false;
    void Update()
    {
        _conductor = FindObjectOfType<Conductor>();

        if (_conductor && !_isInitialized)
        {
            _conductor.onBeat.AddListener(OnBeat);
            _isInitialized = true;
        }
    }

    public abstract void OnBeat();
}