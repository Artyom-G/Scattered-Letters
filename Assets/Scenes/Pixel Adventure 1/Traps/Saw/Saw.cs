using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{

    public float speed = 10;
    public Vector3[] points = new Vector3[2];
    int pointIndex = 0;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 displacement = (points[pointIndex] - transform.position);
        if(displacement.sqrMagnitude < 0.01){
            pointIndex++;
            if(pointIndex >= points.Length)
            {
                pointIndex = 0;
            }
        }
        transform.position += displacement.normalized * speed * Time.deltaTime;
    }
}
