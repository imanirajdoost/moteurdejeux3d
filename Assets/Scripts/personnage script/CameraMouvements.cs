using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouvements : MonoBehaviour
{
    // Start is called before the first frame update
    public double Angle = 0;
    public float RotateSpeed = 0.05f;
    public float StabilisationSpeed = 0.025f;
    public int MAXANGLE = 15;
    private bool translation = false;
    private int max_tr = 5;
    void Start()
    {
        
    }
    void OnTriggerEnter(Collider infoCollision) // le type de la variable est Collision
    {
        translation = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (translation)
        {

        }
        float mH = Input.GetAxis("Horizontal");
        float mV = Input.GetAxis("Vertical");
        if (mH > 0)
        {
            if (Angle > -MAXANGLE)
            {
                transform.Rotate(0, 0, RotateSpeed * -1, Space.Self);
                Angle -= RotateSpeed;
            }
        }
        else if (mH < 0)
        {
            if (Angle < MAXANGLE)
            {
                transform.Rotate(0, 0, RotateSpeed, Space.Self);
                Angle += RotateSpeed;
            }
        }
        else if (Angle > 0)
        {
            transform.Rotate(0, 0, StabilisationSpeed * -1, Space.Self);
            Angle -= StabilisationSpeed;
        }
        else if (Angle < 0)
        {
            transform.Rotate(0, 0, StabilisationSpeed, Space.Self);
            Angle += StabilisationSpeed;

        }
    }
}
