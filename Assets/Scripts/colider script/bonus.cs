using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonus : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
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
            StartCoroutine(waitForDestroy(3));
        }
    }
}
