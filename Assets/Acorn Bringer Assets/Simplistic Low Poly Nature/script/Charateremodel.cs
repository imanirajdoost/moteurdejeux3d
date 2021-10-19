using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charateremodel : MonoBehaviour
{
    // Start is called before the first frame update
    public double Angle = 0;
    public double MAXANGLE = 50;
    public float  RotateSpeed = 0.2f;
    public float StabilisationSpeed = 0.1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //droite 
        if (Input.GetKey(KeyCode.D))
        {
            if (Angle > -MAXANGLE)
            {
                transform.Rotate(0, 0, RotateSpeed*-1, Space.Self);
                Angle -= RotateSpeed ;
            }
        }
        //gauche
        else if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A))
        {
            if (Angle < 50)
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



    }
}
