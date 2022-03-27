using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject target;
    public float speed = 0.125f;
    public Vector3 offset;

    void FixedUpdate()
    {
        Vector3 nextPosition = target.transform.position + offset;
        Vector3 smothNextPosition = Vector3.Lerp(transform.position, nextPosition, this.speed);
        transform.position = smothNextPosition;
    }
}
