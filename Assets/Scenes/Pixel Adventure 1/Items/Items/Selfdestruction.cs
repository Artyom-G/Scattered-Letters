using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selfdestruction : MonoBehaviour
{
    public float Timer = 0.5f;
    void Start()
    {
        Destroy(gameObject, Timer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
