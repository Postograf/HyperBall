using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPool : ObjectPool
{
    [SerializeField] private Platform _platformPrefab;

    private void Start()
    {
        Initialize(_platformPrefab.gameObject);
    }

    private void Update()
    {
        if(IsEmpty)
            DisableObjectsAboardScreen();
    }
}
