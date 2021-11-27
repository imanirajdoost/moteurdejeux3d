using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouvements : MonoBehaviour
{
    // Start is called before the first frame update
    double Angle = 0;// angle auquele ets la camera a l'instant t
    public float RotateSpeed = 0.05f; // vitesse a la quelle la camera tend vers sa rotation min/max 
    public float StabilisationSpeed = 0.025f; // vitesse a la quelle la camera se remet en place 
    public int MAXANGLE = 15; // angle maximal en - et en + de rotation de la camera par rapport a la rotation initial 
    private bool ready = false; // utiliser pour demarer le mouvement de zoom/unzoom 
    private bool dezoom = false; // utiliser pour temporiser entre le zoom et le unzoom 
    int compteur = 0; // compteur pour le zoom/unzoom 
    int maxUNZOOMDISTANCE = 50;


    void Start()
    {
        //initpos=transform.localPosition.z;
    }
    private IEnumerator waitForback(float t)
    {
        //temps de pose entre le zoom et le unzoom 
        yield return new WaitForSeconds(t);
        Debug.Log("fin");
        ready = false;

     }
    //fonction qui demare un effet de vitesse (mouvement arrière de la camera )
    public void zoom()
    {
        ready = true;
        dezoom = false;
    }
    private IEnumerator waitForunzoom(float t)
    {
        yield return new WaitForSeconds(t);
        unzoom();
    }
    //fonction qui remet la camera dans son etat initial (coordonées ) 
    private void unzoom()
    {
        ready = false;
        dezoom = true;
    }
    // Update is called once per frame
    void Update()
    {

        //gestion des unzoom (effet de vitesse qui fait reculer la camera) 
        if (ready )
        {

            transform.Translate(0,0,-1* Time.deltaTime, Space.Self);
            compteur += 1;
            if (compteur > maxUNZOOMDISTANCE)
            {
                ready = false;
                StartCoroutine(waitForunzoom(1));
            }
        }
        else
        {
            if(compteur>0 && dezoom)
            {
                transform.Translate(0, 0, Time.deltaTime, Space.Self);
                compteur -= 1;
            }
        }


        float mH = Input.GetAxis("Horizontal");
        float mV = Input.GetAxis("Vertical");
        //gestion de la rotation de la camera lors des mouvement du joueur 
        if (mH > 0)
        {
            if (Angle > -MAXANGLE)
            {
                transform.Rotate(0, 0, RotateSpeed * -1 *Time.deltaTime, Space.Self);
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
