using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    //private bool est_mort = false;
    public CharatereMovements Cmouv;
    public Charateremodel Cmodel;
    public personnage perso;
    public ParticleSystem par; 


    private void OnEnable()
    {
        //initialisation des Object (ils sont unique dans une mape donnée donc on trouvera toujour le bon )
        if (Cmouv == null)
            Cmouv = FindObjectOfType<CharatereMovements>(true);
        if (Cmodel == null)
            Cmodel = FindObjectOfType<Charateremodel>(true);
        if (perso == null)
            perso = FindObjectOfType<personnage>(true);
    }

    private IEnumerator waitFordead(float t)
    {
        //attendre la fin de l'animation pour recommencer 
        yield return new WaitForSeconds(t);
        GameManager.instance.GameOver();
    }
    void OnTriggerEnter(Collider infoCollision) // le type de la variable est Collision
    {
        if (infoCollision.gameObject.CompareTag("Player"))
        {
            //est_mort = true;
            //prevenir les autres objet de la mort  du joueur et faire les conséquences 
            Cmouv.est_mort = true;
            Cmodel.est_mort = true;
            par.Play();
            perso.mourir(2);
            StartCoroutine(waitFordead(4f));
        }
    }
}
