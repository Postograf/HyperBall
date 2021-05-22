using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;

    private void Update()
    {
        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x, 
            transform.localEulerAngles.y + _rotateSpeed * Time.deltaTime, 
            transform.localEulerAngles.z
        );
    }
}
