using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : BeatResponder
{
    [SerializeField] private GameObject enemyPrefab;
    public float distanceFromEdge = 1f; //distance of the object from the edge of the screen
    private bool isSpawning = false;
    private int numEnemies = 0;

    public override void OnBeat()
    {
        if (isSpawning && _conductor.CurrBeat % 4 == 0 && numEnemies > 0)
        {
            SpawnObject(RandomEdgePosition()); //spawn at a random edge
            numEnemies -= 1;
        }
    }

    public void StartSpawning(int enemies)
    {
        isSpawning = true;
        numEnemies = enemies;
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }

    Vector3 RandomEdgePosition()
    {
        float rand = Random.value;
        Vector3 spawnPosition = Vector3.zero;
        if (rand < 0.25f) //spawn at top edge
        {
            spawnPosition = Camera.main.ScreenToWorldPoint(
                new Vector3(
                    Random.Range(distanceFromEdge, Screen.width - distanceFromEdge), 
                    Screen.height + distanceFromEdge, 
                    0f)
                    );
            return spawnPosition;
        }
        else if (rand < 0.5f) //spawn at right edge
        {
            spawnPosition = Camera.main.ScreenToWorldPoint(
                new Vector3(
                    Screen.width + distanceFromEdge, 
                    Random.Range(distanceFromEdge, 
                    Screen.height - distanceFromEdge), 0f)
                    );
            return spawnPosition; //new Vector3(Screen.width + distanceFromEdge, Random.Range(distanceFromEdge, Screen.height - distanceFromEdge), 0f);
        }
        else if (rand < 0.75f) //spawn at bottom edge
        {
            spawnPosition = Camera.main.ScreenToWorldPoint(
                new Vector3(
                    Random.Range(distanceFromEdge, Screen.width - distanceFromEdge), 
                    -distanceFromEdge, 
                    0f)
                    );
            return spawnPosition; //new Vector3(Random.Range(distanceFromEdge, Screen.width - distanceFromEdge), -distanceFromEdge, 0f);
        }
        else //spawn at left edge
        {
            spawnPosition = Camera.main.ScreenToWorldPoint(
                new Vector3(
                    -distanceFromEdge, 
                    Random.Range(distanceFromEdge, Screen.height - distanceFromEdge), 
                    0f)
                    );
            return spawnPosition; //new Vector3(-distanceFromEdge, Random.Range(distanceFromEdge, Screen.height - distanceFromEdge), 0f);
        }
    }

    void SpawnObject(Vector3 spawnPosition)
    {
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }


    // public List<GameObject> enemiesSpawned;
    // public int Spawn()
    // {
    //     enemiesSpawned.Add(SpawnObject(Screen.width / 2f, Screen.height - distanceFromEdge)); //spawn at top center
    //     enemiesSpawned.Add(SpawnObject(Screen.width - distanceFromEdge, Screen.height / 2f)); //spawn at right center
    //     enemiesSpawned.Add(SpawnObject(Screen.width / 2f, distanceFromEdge)); //spawn at bottom center
    //     enemiesSpawned.Add(SpawnObject(distanceFromEdge, Screen.height / 2f)); //spawn at left center
    //     return enemiesSpawned.Count;
    // }
    // GameObject SpawnObject(float x, float y)
    // {
    //     Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 0f));
    //     return Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    // }
}
