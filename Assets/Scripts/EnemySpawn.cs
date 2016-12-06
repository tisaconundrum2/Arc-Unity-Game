using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

    public float maxSpawnRateInSeconds = 2f;
    public GameObject Enemy;
    public GameObject score;

	// Use this for initialization
	void Start () {

	}

    public void ScheduleEnemySpawner(){
        Invoke("SpawnEnemy", maxSpawnRateInSeconds);
    }

    public void UnscheduleEnemySpawner(){
        CancelInvoke("SpawnEnemy");
        CancelInvoke("IncreaseSpawnRate");
    }

    void SpawnEnemy(){
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));                          // our minimum point in the world is the lower left hand corner
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));                          // our maximum point in the world is the upper right hand corner
        GameObject anEnemy = (GameObject)Instantiate(Enemy);                                        // instantiate a new enemy object
        anEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);                // then have that enenmy slowly descend down the screen
        ScheduleNextEnemySpawn();                                                                   // schedule the next enemy to appear.
    }

    void ScheduleNextEnemySpawn(){
        if (maxSpawnRateInSeconds > 0.2f)                                                            // as long as the maxSpawnRate is above 0.1f
            maxSpawnRateInSeconds = maxSpawnRateInSeconds - 0.01f;                                   // decrease the spawnRate Timeout
        Invoke("SpawnEnemy", maxSpawnRateInSeconds);                                                 // invoke the next spawning.
    }
}
