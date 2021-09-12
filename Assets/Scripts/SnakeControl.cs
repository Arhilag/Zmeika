using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeControl : MonoBehaviour
{
    [SerializeField] Transform target;

    const float followDistance = 1.1f;

    void FixedUpdate()
    {
        transform.position = target.position + (transform.position - target.position).normalized * followDistance;
    }
}
