using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharatereMovements : MonoBehaviour
{
    public float speed = 12f;
    public int skeed = 3;
    public double Angle = 0;
    public float mH;
    public float mV;

    public Rigidbody rb;

    void Update()
    {
        mH = Input.GetAxis("Horizontal");
        mV = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(-speed, mV * speed, mH * speed);
        
    }
}
