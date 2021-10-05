using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeTail : MonoBehaviour
{
    [SerializeField] Transform _target;

    const float _followDistance = 1.1f;

    void FixedUpdate()
    {
        transform.position = _target.position + (transform.position - _target.position).normalized * _followDistance;
    }
}
