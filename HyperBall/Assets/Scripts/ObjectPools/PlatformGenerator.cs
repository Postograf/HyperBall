using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : ObjectPool
{
    [SerializeField] private GameObject _platformPrefab;
    [SerializeField] private float _spawnDistance;
    [SerializeField] private int _startCount;

    private Vector3 _nextSpawnPosition;
    private Vector3 _lastSpawnPosition;

    private void Start()
    {
        _nextSpawnPosition = transform.position;

        Initialize(_platformPrefab);

        for (int i = 0; i < _startCount; i++)
        {
            if (TryGetObject(out GameObject platform))
            {
                SetPlatform(platform);
            }
        }

        _lastSpawnPosition = transform.position;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _lastSpawnPosition) > _spawnDistance)
        {
            if (TryGetObject(out GameObject platform))
            {
                SetPlatform(platform);
                _lastSpawnPosition = transform.position;
                DisableObjectsAboardScreen();
            }
        }
    }

    private void SetPlatform(GameObject platform)
    {
        platform.SetActive(true);
        platform.transform.position = _nextSpawnPosition;
        _nextSpawnPosition.z += platform.transform.localScale.z;
    }
}
