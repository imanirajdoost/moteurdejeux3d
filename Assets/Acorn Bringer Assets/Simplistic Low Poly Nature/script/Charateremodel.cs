using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charateremodel : MonoBehaviour
{
    // Start is called before the first frame update
    public double Angle = 0;
    public double VAngle = 0;
    public double MAXANGLE = 50;
    public float  RotateSpeed = 0.2f;
    public float StabilisationSpeed = 0.1f;
    public int UDS = 8;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mH = Input.GetAxis("Horizontal");
        float mV = Input.GetAxis("Vertical");
        //droite 

        if (mH>0)
        {
            if (Angle > -MAXANGLE)
            {
                transform.Rotate(0, 0, RotateSpeed*-1, Space.Self);
                Angle -= RotateSpeed ;
            }
        }
        //gauche
        else if (mH<0)
        {
            if (Angle < MAXANGLE)
            {
                transform.Rotate(0, 0, RotateSpeed, Space.Self);
                Angle += RotateSpeed;
            }
        }
        else if (Angle > 0)
        {
            transform.Rotate(0, 0, StabilisationSpeed *-1, Space.Self);
            Angle -= StabilisationSpeed;
        }
        else if (Angle < 0)
        {
            transform.Rotate(0, 0, StabilisationSpeed, Space.Self);
            Angle += StabilisationSpeed;

        }
        if (mV > 0)
        {
            if (VAngle > -MAXANGLE/2)
            {
                transform.Rotate(-RotateSpeed/ UDS, 0, 0, Space.Self);
                VAngle -= RotateSpeed/ UDS;
            }
        }
        else if (mV < 0)
        {
            if (VAngle < MAXANGLE/2)
            {
                transform.Rotate(RotateSpeed / UDS, 0, 0, Space.Self);
                VAngle += RotateSpeed / UDS;
            }
        }
        else if (VAngle > 0)
        {
            transform.Rotate(-StabilisationSpeed/ UDS, 0, 0, Space.Self);
            VAngle -= (StabilisationSpeed / UDS);
        }
        else if (VAngle < 0)
        {
            transform.Rotate((StabilisationSpeed/ UDS), 0, 0, Space.Self);
            VAngle += (StabilisationSpeed/ UDS);

        }
        



    }
}
