using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : ObjectPool
{
    [SerializeField] private Obstacle _obstaclePrefab;

    private void Start()
    {
        Initialize(_obstaclePrefab.gameObject);
    }

    private void Update()
    {
        if (!IsFull)
            DisableObjectsAboardScreen();
    }

    public void CreateWave(GameObject platform)
    {
        var positionRangeX = Vector2.zero;
        positionRangeX.x -= platform.transform.localScale.x / 2f - _obstaclePrefab.transform.localScale.x / 2f;
        positionRangeX.y += platform.transform.localScale.x / 2f - _obstaclePrefab.transform.localScale.x / 2f;

        var targetSpawnPoint = platform.transform.position;
        targetSpawnPoint.y += platform.transform.localScale.y;
        targetSpawnPoint.z -= platform.transform.localScale.z / 2 - _obstaclePrefab.transform.localScale.z / 2;

        var obstacleDistance = platform.transform.localScale.z - _obstaclePrefab.transform.localScale.z * Capacity;
        obstacleDistance /= Capacity - 1;

        SpawnObstacles(targetSpawnPoint ,positionRangeX, obstacleDistance);
    }

    private void SpawnObstacles(Vector3 targetSpawnPoint, Vector2 positionRangeX, float obstacleDistance)
    {
        while (TryGetObject(out GameObject obstacle))
        {
            targetSpawnPoint.x = Random.Range(positionRangeX.x, positionRangeX.y);
            SetObstacle(obstacle, targetSpawnPoint);
            targetSpawnPoint.z += obstacleDistance;
        }
    }

    private void SetObstacle(GameObject obstacle, Vector3 position)
    {
        obstacle.SetActive(true);
        obstacle.transform.position = position;
    }
}
