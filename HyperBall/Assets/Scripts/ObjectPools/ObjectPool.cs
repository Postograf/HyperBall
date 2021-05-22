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
    private List<GameObject> _pool = new List<GameObject>();

    public int Capacity => _capacity;

    protected void Initialize(GameObject prefab)
    {
        _disableOffsetZ = prefab.transform.localScale.z / 2;

        _camera = Camera.main;

        for (int i = 0; i < _capacity; i++)
        {
            var spawnedObject = Instantiate(prefab, _container);
            spawnedObject.SetActive(false);

            _pool.Add(spawnedObject);
        }
    }



    protected bool TryGetObject(out GameObject result)
    {
        result = _pool.FirstOrDefault(x => !x.activeSelf);

        return result != null;
    }

    protected void DisableObjectsAboardScreen()
    {
        var disablePoint = _camera.ViewportToWorldPoint(new Vector2(0.5f, 0));

        foreach (var item in _pool)
        {
            if (item.activeSelf)
            {
                if (item.transform.position.z < disablePoint.z - _disableOffsetZ)
                    item.SetActive(false);
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
