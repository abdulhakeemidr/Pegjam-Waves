using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public InterfaceManager ui;
        
    private int _currWave = 0;

    void Start()
    {
        RestartGame();
    }

    private void RestartGame()
    {
        _currWave = 0;
        StartRound(_currWave);
    }

    private void StartRound(int round)
    {
        StartCoroutine(ui.StartRound(_currWave));
    }
}
