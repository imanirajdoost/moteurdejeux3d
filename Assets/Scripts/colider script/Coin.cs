using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public ParticleSystem shines; // particule quand on recup�re la piece 
    public AudioSource audi;   // son qui se joue quand on prend la pi�ce 
    private bool play=false;
    private nbcoin nc;
    // Start is called before the first frame update
    void Start()
    {
        if(nc==null)
            nc=FindObjectOfType<nbcoin>(true);

    }
    private IEnumerator waitForDestroy(float t)
    {
        yield return new WaitForSeconds(t);
        play = false;//puisque l'objet est r�utiliser , il faut le r�initialiser
        gameObject.SetActive(false);
        GameManager.instance.nbcoin++;
        nc.addcoin();
        //arreter les particules et le son (car l'objet sera r�utiliser ) 
        shines.Pause();
        audi.Pause();


    }
    void OnTriggerEnter(Collider infoCollision) // le type de la variable est Collision
    {
        if (infoCollision.gameObject.CompareTag("Player"))
        {
            play = true;
            //quand on recup�re la piece le sond et les particules se d�marent
            shines.Play();
            audi.Play();
            gameObject.transform.localScale = new Vector3(0, 0, 0); //render l'objet invisible 

            StartCoroutine(waitForDestroy(5)); // d�truire l'objet apres quelque temps pour le son et les particules
        }
    }
        // Update is called once per frame
        void Update()
    {

    }
}
