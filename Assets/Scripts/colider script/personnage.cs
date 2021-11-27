using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class personnage : MonoBehaviour
{
    // Start is called before the first frame update
    public int nbVies=1; //nombre de vie du personnage (pas encre utiliser)
    public Animator death;
    public CharatereMovements mouv;
    private bool ISBossted = false;
    public ParticleSystem powerUppartcile;
    /*
    private IEnumerator waitForDestroy(float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(gameObject);
    }
    */
    void Start()
    {
        
    }
    public void mourir(int s)
    {
        death.SetBool("est_mort", true);
    }
    //fonction qui attend et décélère 
    private IEnumerator waitForSlowdown(float t)
    {
        yield return new WaitForSeconds(t);
        mouv.speed-=5;
        ISBossted = false;
        //powerUppartcile.Pause();
        powerUppartcile.Stop();

    }
    void OnTriggerEnter(Collider infoCollision) // le type de la variable est Collision
    {
           //détéction des colision avec le speed up 
        if (infoCollision.gameObject.CompareTag("SpeedUp"))
        {
            //pour qu'on ne puisse pas cumuler les boostes (je pens epas que ce soit une bonne idée ) 
            if (!ISBossted)
            {
                if (powerUppartcile != null)
                    powerUppartcile.Play();
                //on accélère 
                mouv.speed += 5;
                ISBossted = true;
                //on attend pour décélérer 
                StartCoroutine(waitForSlowdown(5));
            }
        }
        /*else if (nbVies>0 && infoCollision.gameObject.CompareTag("obstacle"))
        {
            //infoCollision.gameObject.SetActive(false);
            nbVies--;
            if (nbVies == 0)
                mourir(10);
        }
        else if (infoCollision.gameObject.name == "Eau")
        {
            infoCollision.gameObject.SetActive(false);
        }
        else if (infoCollision.gameObject.name == "coin")
        {
            nbcoin++;
            Debug.Log(nbcoin);
        }*/
    }
}
