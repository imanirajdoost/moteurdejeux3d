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
    public AudioSource ComboSound;
    public AudioSource DeadSound;
    /*
    private IEnumerator waitForDestroy(float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(gameObject);
    }
    */
    private int actualSpeed = 0;
    void Start()
    {
        
    }
    public void mourir(int s)
    {
        if (DeadSound != null)
            DeadSound.Play();

        death.SetBool("est_mort", true);
    }
    //fonction qui attend et décélère 
    private IEnumerator waitForSlowdown(float t)
    {
        yield return new WaitForSeconds(t);
        //réinitialisation de la vitesse 
        mouv.speed-= actualSpeed;
        actualSpeed = 0;
        ISBossted = false;
        //powerUppartcile.Pause();
        powerUppartcile.Stop();

    }
    void OnTriggerEnter(Collider infoCollision) // le type de la variable est Collision
    {
           //détéction des colision avec le speed up 
        if (infoCollision.gameObject.CompareTag("SpeedUp"))
        {
            //pour qu'on ne puisse pas cumuler les boostes (je pense pas que ce soit une bonne idée ) 
            if (!ISBossted)
            {
                if (powerUppartcile != null)
                    powerUppartcile.Play();
                //on accélère 
                actualSpeed += 5;
                mouv.speed += 5;
                ISBossted = true;
                //on attend pour décélérer 
                StartCoroutine(waitForSlowdown(5));
            }
            else
            {
                //if on a deja ete accelerer juste augmenter la vitesse de 2 
                //sans oublier de le dire a actual speed pour pouvoir reset la vitesse initial 
                actualSpeed += 2;
                mouv.speed += 2;
                if (actualSpeed > 10 && ComboSound!=null && !ComboSound.isPlaying)
                    ComboSound.Play();

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
