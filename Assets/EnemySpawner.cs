using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    public float distanceFromEdge = 1f; //distance of the object from the edge of the screen

    void Start()
    {
        SpawnObject(Screen.width / 2f, Screen.height - distanceFromEdge); //spawn at top center
        SpawnObject(Screen.width - distanceFromEdge, Screen.height / 2f); //spawn at right center
        SpawnObject(Screen.width / 2f, distanceFromEdge); //spawn at bottom center
        SpawnObject(distanceFromEdge, Screen.height / 2f); //spawn at left center
    }

    void SpawnObject(float x, float y)
    {
        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 0f));
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
