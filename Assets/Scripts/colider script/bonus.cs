using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonus : MonoBehaviour
{
    private Animator anim;
    private bool moov =false;
    private int decal=0;
    public int vitesse = 1;
    public CameraMouvements cam;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        if (cam == null)
            cam = FindObjectOfType<CameraMouvements>(true);
    }
    private IEnumerator waitForDestroy(float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(gameObject);
    }
        void OnTriggerEnter(Collider infoCollision) // le type de la variable est Collision
    {
        //Debug.Log("HELLLLO FROM BONUSSSSSSSSSSSSSSSSSSSSSS");
        if (infoCollision.gameObject.CompareTag("Player"))
        {
                anim.SetTrigger("Score");
            cam.zoom();
            StartCoroutine(waitForDestroy(1));
        }
    }
}
