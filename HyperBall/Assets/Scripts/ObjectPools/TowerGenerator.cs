using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerGenerator : ObjectPool
{
    [SerializeField] private Tower _towerPrefab;
    [SerializeField] private Vector2 _rangeY;
    [SerializeField] private float _distanceToPlatform;
    [SerializeField] private float _edgeOffsetZ;
    [SerializeField] private int _layersCountOnSide;
    
    private int _rowsCount = 3;
    private Vector3 _nextTowerPosition;

    private void Start()
    {
        Initialize(_towerPrefab.gameObject);
    }

    private void Update()
    {
        if(!IsFull)
            DisableObjectsAboardScreen();
    }

    public void CreateCity(GameObject platform)
    {
        _nextTowerPosition = platform.transform.position;

        _nextTowerPosition.x += platform.transform.localScale.x / 2 + _distanceToPlatform + _towerPrefab.transform.localScale.x / 2;
        _nextTowerPosition.z -= platform.transform.localScale.z / 2 - _edgeOffsetZ;

        var distanceBetweenRows = platform.transform.position.z - _nextTowerPosition.z;

        for (int k = 0; k < _rowsCount; k++)
        {
            for (int i = 0; i < _layersCountOnSide; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    _nextTowerPosition.x *= -1;

                    if (TryGetObject(out GameObject tower))
                    {
                        _nextTowerPosition.y = Random.Range(_rangeY.x, _rangeY.y);
                        SetTower(tower);
                    }
                }
                _nextTowerPosition.x += _distanceToPlatform + _towerPrefab.transform.localScale.x;
            }
            _nextTowerPosition.z += distanceBetweenRows;
        }
    }

    private void SetTower(GameObject tower)
    {
        tower.SetActive(true);
        tower.transform.position = _nextTowerPosition;
    }
}
