using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] GameObject enemy;
    [SerializeField] float timeBetweenWaves;
    [SerializeField] int[] enemiesPerWave;
    [Range(0, 15)]
    [SerializeField] int maxEnemies;

    [SerializeField] List<GameObject> availableEnemies = new List<GameObject>();
    List<Transform> spawnPoints = new List<Transform>();
    float previousTime = 0f;
    int waveNumber = 0;
    // Update is called once per frame

    void Start()
    {
        foreach (Transform child in transform)
        {
            spawnPoints.Add(child);
        }

        for (int i = 0; i < maxEnemies; i++)
        {
            GameObject go = Instantiate(enemy);
            go.SetActive(false);
            availableEnemies.Add(go);
        }
    }

    void Update()
    {
        if (timeBetweenWaves == 0f || enemiesPerWave.Length <= waveNumber)
            return;

        if ((Time.time - previousTime > timeBetweenWaves || waveNumber == 0) &&
            availableEnemies.Count >= enemiesPerWave[waveNumber])
        {
            for (int i = 0; i < enemiesPerWave[waveNumber]; i++)
            {
                if (availableEnemies.Count > 0)
                {
                    GameObject go = availableEnemies[0];
                    go.transform.position = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
                    go.SetActive(true);
                    availableEnemies.Remove(go);
                }
            }
            previousTime = Time.time;
            waveNumber++;
        }
    }

    public void AddToActive(GameObject go)
    {
        go.SetActive(false);
        availableEnemies.Add(go);
    }
}