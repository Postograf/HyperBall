using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private int _capacity;
    
    private Camera _camera;
    private float _disableOffsetZ;
    private int _activeObjects;
    private List<GameObject> _pool = new List<GameObject>();

    public int Capacity => _capacity;
    public bool IsFull => _activeObjects == 0;
    public bool IsEmpty => _capacity == _activeObjects;

    protected void Initialize(GameObject prefab)
    {
        _disableOffsetZ = prefab.transform.localScale.z;

        _camera = Camera.main;

        for (int i = 0; i < _capacity; i++)
        {
            var spawnedObject = Instantiate(prefab, _container);
            spawnedObject.SetActive(false);

            _pool.Add(spawnedObject);
        }
    }

    public bool TryGetObject(out GameObject result)
    {
        result = _pool.FirstOrDefault(x => !x.activeSelf);

        if (result != null)
        {
            _activeObjects++;
            return true;
        }

        return false;
    }

    protected void DisableObjectsAboardScreen()
    {
        var disablePoint = _camera.ViewportToWorldPoint(new Vector2(0.5f, 0));

        foreach (var item in _pool)
        {
            if (item.activeSelf)
            {
                if (item.transform.position.z < disablePoint.z - _disableOffsetZ)
                {
                    item.SetActive(false);
                    _activeObjects--;
                }
            }
        }
    }

    public void ResetPool()
    {
        foreach(var item in _pool)
        {
            item.SetActive(false);
        }
    }
}
