using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonus : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }
    private IEnumerator waitForDestroy(float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(gameObject, 50f);
    }
        void OnTriggerEnter(Collider infoCollision) // le type de la variable est Collision
    {
        if (infoCollision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(waitForDestroy(10));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
