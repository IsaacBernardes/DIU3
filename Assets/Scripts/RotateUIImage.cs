using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateUIImage : MonoBehaviour
{
    
    private RectTransform rect;
    private float zRotation;

    void Start()
    {
        this.rect = GetComponent<RectTransform>();
        this.zRotation = Random.Range(0, 360);
    }

    void FixedUpdate()
    {
        this.rect.rotation = Quaternion.Euler(0, 0, this.zRotation++);
    }
}
