using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTracker : MonoBehaviour
{
    [SerializeField] private BallMover _ball;
    [SerializeField] private float _neededDistance;
    [SerializeField] private float _speed = 1;

    private void FixedUpdate()
    {
        if (_ball == null) 
            return;
        
        var targetPosition = new Vector3(_ball.transform.position.x, transform.position.y, _ball.transform.position.z - _neededDistance);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
    }
}
