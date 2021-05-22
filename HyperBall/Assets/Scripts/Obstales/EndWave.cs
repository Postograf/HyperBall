using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndWave : MonoBehaviour
{
    public UnityAction<EndWave> WaveEnded;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BallMover ball))
        {
            WaveEnded?.Invoke(this);
        }
    }
}
