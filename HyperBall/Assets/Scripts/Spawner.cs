using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using static UnityEditor.Experimental.GraphView.Port;

public class Spawner : MonoBehaviour
{
    [SerializeField] private PlatformPool _platformPool;
    [SerializeField] private ObstacleGenerator[] _obstaclePools;
    [SerializeField] private TowerGenerator _towerGenerator;
    [SerializeField] private Bonus _bonusPrefab;

    private Vector3 _nextSpawnPosition;
    private int _platformCount;

    private void Start()
    {
        _nextSpawnPosition = transform.position;
    }

    private void Update()
    {
        if(_platformPool.TryGetObject(out GameObject platform))
        {
            SetPlatform(platform);
            _platformCount++;

            if (_platformCount % 2 == 0)
            {
                _obstaclePools
                    .Where(pool => pool.IsFull)
                    .FirstOrDefault()
                    ?.CreateWave(platform);
            }
            else
            {
                SpawnBonus(platform);
            }

            _towerGenerator.CreateCity(platform);
        }
    }

    private void SetPlatform(GameObject platform)
    {
        platform.SetActive(true);
        platform.transform.position = _nextSpawnPosition;
        _nextSpawnPosition.z += platform.transform.localScale.z;
    }

    private void SpawnBonus(GameObject platform)
    {
        var scaleX = platform.transform.localScale.x / 2;
        var scaleZ = platform.transform.localScale.z / 2;
        var x = platform.transform.position.x + Random.Range(-scaleX, scaleX);
        var z = platform.transform.position.z + Random.Range(-scaleZ, scaleZ);
        var y = platform.transform.position.y + platform.transform.localScale.y / 2 + _bonusPrefab.transform.localScale.x * 1.5f;

        var position = new Vector3(x, y, z);
        Instantiate(_bonusPrefab, position, _bonusPrefab.transform.rotation);
    }
}
