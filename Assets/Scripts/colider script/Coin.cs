using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public ParticleSystem shines;
    public AudioSource audio;
    public bool play=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private IEnumerator waitForDestroy(float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(gameObject);

    }
    void OnTriggerEnter(Collider infoCollision) // le type de la variable est Collision
    {
        if (infoCollision.gameObject.CompareTag("Player"))
        {
            play = true;
            gameObject.transform.localScale = new Vector3(0, 0, 0); //render l'objet invisible 
            StartCoroutine(waitForDestroy(1));
        }
    }
        // Update is called once per frame
        void Update()
    {
        if (play)
        {
            if (!shines.isPlaying)
                shines.Play();
            if(!audio.isPlaying)
                audio.Play();

        }
        else
        {
            if (shines.isPlaying)
                shines.Pause();
            if (audio.isPlaying)
                audio.Pause();
        }

    }
}
