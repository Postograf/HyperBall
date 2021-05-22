using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] private Obstacle[] _obstacles;
    [SerializeField] private GameObject _platform;
    [SerializeField] private Transform _parent;
    [SerializeField] private float _startDistanceDelay;
    [SerializeField] private float _distanceBetweenWave;
    [SerializeField] private int _obstacleCountInWave;
    [SerializeField] private float _obstacleDistance;

    private Vector3 _targetSpawnPoint;
    private Vector2 _positionRangeX;

    private void Start()
    {
        _targetSpawnPoint.y += _platform.transform.localScale.y;
        _targetSpawnPoint.z = transform.position.z + _startDistanceDelay;

        CreateWave();
    }

    private void CreateWave()
    {
        int obstacleIndex = Random.Range(0, _obstacles.Length);

        _positionRangeX = Vector2.zero;
        _positionRangeX.x -= _platform.transform.localScale.x / 2f - _obstacles[obstacleIndex].transform.localScale.x / 2f;
        _positionRangeX.y += _platform.transform.localScale.x / 2f - _obstacles[obstacleIndex].transform.localScale.x / 2f;

        SpawnObstacles(_obstacles[obstacleIndex], _obstacleCountInWave);
        CreateEndWaveTrigger(_targetSpawnPoint);
    }

    private void SpawnObstacles(Obstacle obstacle, int countInWave)
    {
        for (int i = 0; i < countInWave; i++)
        {
            _targetSpawnPoint.x = Random.Range(_positionRangeX.x, _positionRangeX.y);
            Instantiate(obstacle, _targetSpawnPoint, Quaternion.identity, _parent);
            _targetSpawnPoint.z += _obstacleDistance;
        }
    }

    private void CreateEndWaveTrigger(Vector3 spawnPoint)
    {
        spawnPoint.x = 0;
        var endWaveTrigger = new GameObject();
        endWaveTrigger.transform.position = spawnPoint;
        endWaveTrigger.AddComponent<BoxCollider>().isTrigger = true;
        endWaveTrigger.AddComponent<EndWave>().WaveEnded += OnWaveEnded;
    }

    private void OnWaveEnded(EndWave endWave)
    {
        endWave.WaveEnded -= OnWaveEnded;
        CreateWave();
    }
}
