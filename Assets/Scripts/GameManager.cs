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
    public List<int> roundBpm = new List<int>();

    public List<Sprite> roundSprites = new List<Sprite>();
    public SpriteRenderer background; 
        
    private int _currRound = 0;
    private const int NumRounds = 10;
    private int _remainingEnemies = 10;
    public int EnemiesSpawned = 0;
    public int EnemiesPerWave = 10;

    public GameObject prefabEnemy;
    public GameObject prefabPlayer;

    private Unit _player;
    public EnemySpawner spawnEnemies;

    void Start()
    {
        RestartGame();
    }

    void Update()
    {
        ui.UpdateHealth(_player.health <= 0 ? 0 : _player.health);
        ui.UpdateEnemiesKilled(EnemiesPerWave - _remainingEnemies);
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
        var assetIndex = (_currRound - 1) % roundSongs.Count;
        background.sprite = roundSprites[assetIndex];

        yield return ui.StartRound(_currRound);
        _remainingEnemies = EnemiesPerWave;

        conductor.LoadSong(new Song(roundSongs[assetIndex], 120));
        conductor.Play();

        spawnEnemies.StartSpawning();
        while (_player.IsAlive && _remainingEnemies > 0)
        {
            yield return new WaitForEndOfFrame();
        }
        spawnEnemies.StopSpawning();

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

    public void ReduceRemainingEnemies()
    {
        _remainingEnemies -= 1;
        if (_remainingEnemies < 0) _remainingEnemies = 0;
    }

    private void SpawnPlayer()
    {
        var objPlayer = Instantiate(prefabPlayer, new Vector3(0.5f, 0.5f, 0.0f), Quaternion.identity);
        _player = objPlayer.GetComponent<Unit>();
    }
}
