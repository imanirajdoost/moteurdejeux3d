using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Manages chicken animation
/// By  Ahmad JREDA
/// </summary>
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
    private MoveCondor Condor;
    /*
    private IEnumerator waitForDestroy(float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(gameObject);
    }
    */
    private int actualSpeed = 0;
    private void Awake()
    {
        if(Condor==null)
            FindObjectOfType<CharatereMovements>(true);
    }
    void Start()
    {
        
    }
    public void mourir(int s)
    {
        if (DeadSound != null)
            DeadSound.Play();

        death.SetBool("est_mort", true);
    }
    public bool est_vivant()
    {
        return !death.GetBool("est_mort");
    }
    //fonction qui attend et d�c�l�re 
    private IEnumerator waitForSlowdown(float t)
    {
        yield return new WaitForSeconds(t);
        //r�initialisation de la vitesse 
        mouv.speed-= actualSpeed;
        actualSpeed = 0;
        ISBossted = false;
        //powerUppartcile.Pause();
        powerUppartcile.Stop();

    }
    void OnTriggerEnter(Collider infoCollision) // le type de la variable est Collision
    {
           //d�t�ction des colision avec le speed up 
        if (infoCollision.gameObject.CompareTag("SpeedUp"))
        {
            //pour qu'on ne puisse pas cumuler les boostes (je pense pas que ce soit une bonne id�e ) 
            if (!ISBossted)
            {
                if (powerUppartcile != null)
                    powerUppartcile.Play();
                //on acc�l�re 
                actualSpeed += 5;
                mouv.speed += 5;
                ISBossted = true;
                //on attend pour d�c�l�rer 
                StartCoroutine(waitForSlowdown(5));
            }
            else
            {
                //if on a deja ete accelerer juste augmenter la vitesse de 2 
                //sans oublier de le dire a actual speed pour pouvoir reset la vitesse initial 
                actualSpeed += 2;
                mouv.speed += 2;
                if (actualSpeed > 8 && ComboSound!=null && !ComboSound.isPlaying)
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
