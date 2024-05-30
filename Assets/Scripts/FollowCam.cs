using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public float damping;

    public Transform target;
    Vector3 offset;

    private void Start()
    {
        offset = transform.position - target.position;
        target = GameObject.Find("Player").transform;
    }


    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, damping);
    }
}
