using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharatereMovements : MonoBehaviour
{
    public int speed = 12;
    public int skeed = 3;
    public double Angle = 0;
    public float mH;
    public float mV;

    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        mH = Input.GetAxis("Horizontal");
        mV = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(mH * speed, mV * speed,  speed);
        
    }
}
