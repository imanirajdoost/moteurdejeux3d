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
    public GameObject objectRenderer;       //Bomb main mesh renderer
    public LayerMask clickLayerMask;

    private MoveCondor condorManager;
    private SoundManager soundManager;
    private bool isActive;

    private void OnEnable()
    {
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
            KillPlayer();
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
                        Explode();
                        break;
                    }
                }
            }
        }
    }

    private void KillPlayer()
    {
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
        objectRenderer.SetActive(false);
    }

    private void EnableRenderer()
    {
        objectRenderer.SetActive(true);
    }
}
