using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Manages SpeedUP
/// By Ahmad JREDA
/// </summary>
public class bonus : MonoBehaviour
{
    //private Animator anim;
    public CameraMouvements cam;
    public ParticleSystem shines; // particule qui se joue quand on entre dans l'anneau 
    public AudioSource audi;   // son qui se joue quand on on entre dans l'anneau 


    private void Awake()
    {
        //anim = GetComponent<Animator>();
        //initialisation de la camera du joueur 
        if (cam == null)
            cam = FindObjectOfType<CameraMouvements>(true);
    }
    //fonction qui attend et qui désactive l'objet
    private IEnumerator waitForDestroy(float t)
    {
        yield return new WaitForSeconds(t);
        gameObject.SetActive(false);
    }
        void OnTriggerEnter(Collider infoCollision) // le type de la variable est Collision
    {
        //Debug.Log("HELLLLO FROM BONUSSSSSSSSSSSSSSSSSSSSSS");
        if (infoCollision.gameObject.CompareTag("Player"))
        {
            //anim.SetTrigger("Score");
            //jouer le son et les particules liée au speed up
            if(shines!=null)
                shines.Play();
            if(audi!=null)
                audi.Play();
            // faire bouger la camera 
            cam.zoom();
            //desactiver l'objet apres quelque secondes 
            StartCoroutine(waitForDestroy(1));
        }
    }
}
