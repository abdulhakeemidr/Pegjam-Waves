using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public InterfaceManager ui;
    public Conductor conductor;
    
    [SerializeField]
    public List<AudioClip> roundSongs = new List<AudioClip>();
        
    private int _currRound = 0;
    private const int NumRounds = 10;
    private int _remainingEnemies = 0;
    private int _enemiesSpawned = 0;
    private const int EnemiesPerWave = 100;

    public GameObject prefabEnemy;
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
        
        conductor.LoadSong(new Song(roundSongs[_currRound - 1 % roundSongs.Count], 120));
        conductor.Play();

        while (player.IsAlive && _remainingEnemies > 0)
        {
            yield return new WaitForSeconds(1000);
            
            // TODO: Spawn enemies on interval... update _remainingEnemies on enemy death
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
