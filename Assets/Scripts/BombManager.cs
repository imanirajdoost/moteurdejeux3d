using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages bomb behavior
///By Iman IRAJ DOOST
/// </summary>
public class BombManager : MonoBehaviour
{
    public ParticleSystem explosion;        //Bomb explostion particle effect
    public SkinnedMeshRenderer objectRenderer;       //Bomb main mesh renderer
    public LayerMask clickLayerMask;
    private CharatereMovements Cmouv;
    private Charateremodel Cmodel;
    private MoveCondor condorManager;
    private SoundManager soundManager;
    private bool isActive;
    public ParticleSystem par;
    private personnage perso;
    private void OnEnable()
    {
        if (Cmouv == null)
            Cmouv = FindObjectOfType<CharatereMovements>(true);
        if (Cmodel == null)
            Cmodel = FindObjectOfType<Charateremodel>(true);
        if (perso == null)
            perso = FindObjectOfType<personnage>(true);
        //Get condor
        if (condorManager == null)
            condorManager = FindObjectOfType<MoveCondor>(true);

        if (soundManager == null)
            soundManager = FindObjectOfType<SoundManager>();

        explosion.Stop();
        isActive = true;
        EnableRenderer();
    }

    private void OnDisable()
    {
        explosion.Stop();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (isActive && other.CompareTag("Player"))
        {
            Explode();
            if (par != null)
                par.Play();
            perso.mourir(2);
            Cmouv.est_mort = true;
            Cmodel.est_mort = true;
            StartCoroutine(waitFordead(4f));
        }
    }

    private void Update()
    {
        if (isActive && Camera.main != null && Input.GetMouseButtonDown(0))
        {
            RaycastHit[] hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hit = Physics.RaycastAll(ray, 50f, clickLayerMask);
            if (hit != null && hit.Length > 0)
            {
                for (int i = 0; i < hit.Length; i++)
                {
                    if (hit[i].transform.CompareTag("Bomb") && hit[i].transform.gameObject == this.gameObject)
                    {
                        GetScore();
                        Explode();
                        break;
                    }
                }
            }
        }
    }

    private void GetScore()
    {
        ScoreManager.instance.AddScore(10);
    }

    private IEnumerator waitFordead(float t)
    {
        //attendre la fin de l'animation pour recommencer 
        yield return new WaitForSeconds(t);
        GameManager.instance.GameOver();
    }

    private void Explode()
    {
        if (isActive)
        {
            isActive = false;
            explosion.Play();
            //Slow down condor if he is nearby
            if (Mathf.Abs(Vector2.Distance(transform.position, condorManager.transform.position)) < 6f)
                condorManager.SlowDown();
            soundManager.PlayBombSound();
            DisableRenderer();
            StartCoroutine(DisableAfterT(1f));
        }
    }
    
    private IEnumerator DisableAfterT(float t)
    {
        yield return new WaitForSeconds(t);
        transform.parent.gameObject.SetActive(false);
    }

    private void DisableRenderer()
    {
        objectRenderer.enabled = false;
    }

    private void EnableRenderer()
    {
        objectRenderer.enabled = true;
    }
}
