using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject enemyPrefab;
    public GameObject playerPrefab;

    public float spawnTime = 15.0f;

    public float timeSinceSpawn = 0.0f;

    public float minRadius = 20.0f;
    public float maxRadius = 40.0f;

    public float playerSpawnTime = 1.0f;

    public Transform spawnCenter;

	// Use this for initialization
	void Start () {
        Invoke("SpawnPlayer", playerSpawnTime);
	}
	
	// Update is called once per frame
	void Update () {
        UpdateSpawnTimer();
	}

    void SpawnPlayer()
    {
        // Make left player
        GameObject go = GameObject.Instantiate(playerPrefab);
        GunPlayer player = go.GetComponentInChildren<GunPlayer>();
        player.Initialize(true);

        // Make right player
        go = GameObject.Instantiate(playerPrefab);
        player = go.GetComponentInChildren<GunPlayer>();
        player.Initialize(false);

    }

    void DoSpawn()
    {
        Vector2 offset = Random.Range(minRadius, maxRadius) * Random.insideUnitCircle.normalized;
        Vector3 spawnPoint = spawnCenter.position + new Vector3(offset.x, 0, offset.y);
        GameObject newEnemy = GameObject.Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
    }

    void UpdateSpawnTimer()
    {
        timeSinceSpawn += Time.deltaTime;
        if (timeSinceSpawn >= spawnTime)
        {
            DoSpawn();
            timeSinceSpawn = 0.0f;
        }
    }
}
