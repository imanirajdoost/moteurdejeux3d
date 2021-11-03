using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class personnage : MonoBehaviour
{
    // Start is called before the first frame update
    public int nbVies=1;
    public Animator death;

    void Start()
    {
        
    }
    void mourir(int s)
    {
<<<<<<< HEAD
        
=======
        //death.Play();
>>>>>>> c5280e58e7c93e1fa69087c52511a468684541a2
    }
    void OnTriggerEnter(Collider infoCollision) // le type de la variable est Collision
    {
        if (infoCollision.gameObject.CompareTag("PowerUp"))
        {
            infoCollision.gameObject.SetActive(false);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
