using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public ObjectPool obstaclePool;

    // lowest point an object can spawn
    public float minSpawnY;

    // max height an object can spawn
    public float maxSpawnY;

    // left hand side X spawn pos
    private float leftSpawnX;

    // right hand side X spawn pos
    private float rightSpawnX;

    // rate at which objects are spawned
    public float spawnRate;
    private float lastSpawnTime;

    void Start ()
    {
        Camera cam = Camera.main;
        float camWidth = (2.0f * cam.orthographicSize) * cam.aspect;

        leftSpawnX = -camWidth / 2;
        rightSpawnX = camWidth / 2;
    }

    void Update ()
    {
        if(Time.time - spawnRate >= lastSpawnTime)
        {
            lastSpawnTime = Time.time;
            SpawnObstacle();
        }
    }

    // creates a new obstacle
    void SpawnObstacle ()
    {
        // get the obstacle
        GameObject obstacle = obstaclePool.GetPooledObject();

        // set its position
        obstacle.transform.position = GetSpawnPosition();

        // set the obstacle's direction to move
        obstacle.GetComponent<Obstacle>().moveDir = new Vector3(obstacle.transform.position.x > 0 ? -1 : 1, 0, 0);
    }

    // return a random position to spawn in
    Vector3 GetSpawnPosition ()
    {
        float x = Random.Range(0, 2) == 1 ? leftSpawnX : rightSpawnX;
        float y = Random.Range(minSpawnY, maxSpawnY);

        return new Vector3(x, y, 0);
    }
}