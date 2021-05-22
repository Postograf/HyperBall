using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _deflectionSpeed;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _rigidbody.velocity = new Vector3(0, 0, _moveSpeed);

        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            _rigidbody.velocity = new Vector3(
                touch.deltaPosition.x * _deflectionSpeed,
                _rigidbody.velocity.y,
                _rigidbody.velocity.z
            );
        }
    }
}
