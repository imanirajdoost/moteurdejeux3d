using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private bool est_mort = false;
    public CharatereMovements Cmouv;
    public Charateremodel Cmodel;
    public personnage perso;


    private void OnEnable()
    {
        if (Cmouv == null)
            Cmouv = FindObjectOfType<CharatereMovements>(true);
        if (Cmodel == null)
            Cmodel = FindObjectOfType<Charateremodel>(true);
        if (perso == null)
            perso = FindObjectOfType<personnage>(true);
    }

    private IEnumerator waitFordead(float t)
    {
        yield return new WaitForSeconds(t);
        GameManager.instance.GameOver();
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
}
