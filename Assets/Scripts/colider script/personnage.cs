using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class personnage : MonoBehaviour
{
    // Start is called before the first frame update
    public int nbVies=1;
    public Animator death;
    public CharatereMovements mouv;

    void Start()
    {
        
    }
    void mourir(int s)
    {
    }

    private IEnumerator waitForSlowdown(float t)
    {
        yield return new WaitForSeconds(t);
        mouv.speed-=4;

    }
    void OnTriggerEnter(Collider infoCollision) // le type de la variable est Collision
    {
        Debug.Log("détécté");
        if (infoCollision.gameObject.CompareTag("SpeedUp"))
        {
            mouv.speed+=5;
            StartCoroutine(waitForSlowdown(5));
        }
        else if (nbVies>0 && infoCollision.gameObject.CompareTag("obstacle"))
        {
            infoCollision.gameObject.SetActive(false);
            nbVies--;
            if (nbVies == 0)
                mourir(10);
        }
        else if (infoCollision.gameObject.name == "Eau")
        {
            infoCollision.gameObject.SetActive(false);
        }
    }
}
