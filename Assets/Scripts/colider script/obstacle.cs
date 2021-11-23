using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject ds;
    private bool est_mort = false;
    public CharatereMovements Cmouv;
    public Charateremodel Cmodel;
    public personnage perso;


    // Start is called before the first frame update
    void Start()
    {
        if(est_mort)
        {

        }
    }
    //jouer animation de mort 
    void mourir()
    {
        return;
    }
    private IEnumerator waitFordead(float t)
    {
        yield return new WaitForSeconds(t);
        ds.SetActive(true);
        Time.timeScale = 0;
    }
    void OnTriggerEnter(Collider infoCollision) // le type de la variable est Collision
    {
        if (infoCollision.gameObject.CompareTag("Player"))
        {
            est_mort = true;
            Cmouv.est_mort = true;
            Cmodel.est_mort = true;
            perso.mourir(2);
            StartCoroutine(waitFordead(4f));

            
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
