using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class GameManager : MonoBehaviour
{
    public InterfaceManager ui;
    public Conductor conductor;
    
    [SerializeField]
    public List<AudioClip> roundSongs = new List<AudioClip>();
        
    private int _currRound = 0;
    private const int NumRounds = 10;
    private int _remainingEnemies = 0;
    [SerializeField] private int _enemiesSpawned = 0;
    private const int EnemiesPerWave = 100;

    public GameObject prefabEnemy;
    public GameObject prefabPlayer;

    private PlayerUnit _player;
    public EnemySpawner spawnEnemies;

    void Start()
    {
        RestartGame();
    }

    private void RestartGame()
    {
        if (_player != null) Destroy(_player.gameObject);
        SpawnPlayer();
        
        _currRound = 1;
        StartCoroutine(StartRound());
    }

    private IEnumerator StartRound()
    {
        yield return ui.StartRound(_currRound);
        _remainingEnemies = EnemiesPerWave;
        
        conductor.LoadSong(new Song(roundSongs[(_currRound - 1) % roundSongs.Count], 120));
        conductor.Play();

        while (_player.IsAlive && _remainingEnemies > 0)
        {
            // TODO: Spawn enemies on interval... update _remainingEnemies on enemy death
            spawnEnemies.Spawn();
            yield return new WaitForSeconds(10);
        }

        if (_currRound <= NumRounds && _player.IsAlive)
        {
            yield return ui.EndRound(_currRound);
            _currRound += 1;
            yield return StartRound();
        }
        else
        {
            if (!_player.IsAlive)
                yield return ui.ShowGameOver();
            
            SceneManager.LoadScene("StartScene");
        }
    }

    private void SpawnPlayer()
    {
        var objPlayer = Instantiate(prefabPlayer, new Vector3(0.5f, 0.5f, 0.0f), Quaternion.identity);
        _player = objPlayer.GetComponent<PlayerUnit>();
    }
}
