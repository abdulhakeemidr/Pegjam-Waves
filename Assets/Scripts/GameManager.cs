using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public InterfaceManager ui;
        
    private int _currRound = 0;
    private const int NumRounds = 10;
    private int _remainingEnemies = 0;
    private const int EnemiesPerWave = 100;
    public PlayerUnit player; 
    

    void Start()
    {
        RestartGame();
    }

    private void RestartGame()
    {
        _currRound = 1;
        StartCoroutine(StartRound());
    }

    private IEnumerator StartRound()
    {
        yield return ui.StartRound(_currRound);
        _remainingEnemies = EnemiesPerWave;

        while (player.IsAlive && _remainingEnemies > 0)
        {
            yield return new WaitForSeconds(1000);
        }

        if (_currRound <= NumRounds && player.IsAlive)
        {
            yield return ui.EndRound(_currRound);
            _currRound += 1;
            yield return StartRound();
        }
        else
        {
            if (!player.IsAlive)
                yield return ui.ShowGameOver();
            
            RestartGame();
        }
    }
}
