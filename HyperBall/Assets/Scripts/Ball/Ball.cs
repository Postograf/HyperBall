using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            Destroy(gameObject);
            Instantiate(_particle, transform.position, _particle.transform.rotation);
        }
    }
}
