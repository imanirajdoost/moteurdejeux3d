using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the condor movement
/// By Iman IRAJ DOOST
/// </summary>
public class MoveCondor : MonoBehaviour
{
    public bool moveForward;
    public bool moveDown;
    public bool moveUp;

    public float forwardSpeed = 1f;
    public float downSpeed = 2f;
    public float upSpeed = 4f;

    public GameObject chickChild;
    public ParticleSystem hitEffect;
    private SoundManager soundManager;
    public Animator hitAnim;
    public Animator sliderAnim;

    private BoxCollider col;

    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
        col = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (moveForward)
            // Move the object forward along its z axis 1 unit/second.
            transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        if (moveDown)
            transform.Translate(Vector3.down * downSpeed * Time.deltaTime);

        if (moveUp)
            transform.Translate(Vector3.up * upSpeed * Time.deltaTime);
    }

    public void ChangeSpeed(float newSpeed)
    {
        forwardSpeed = newSpeed;
        StartCoroutine(EnableColliderAfter(5f));
    }

    private IEnumerator EnableColliderAfter(float t)
    {
        yield return new WaitForSeconds(t);
        col.enabled = true;
    }

    public void SlowDown()
    {
        forwardSpeed -= 0.2f;
        hitEffect.Play();
        soundManager.PlayHitSound();
        if (hitAnim != null)
            hitAnim.SetTrigger("Hit");
        if (sliderAnim != null)
            sliderAnim.SetTrigger("Hit");
    }

    public void PickupChick(ChickenAnimationManager chick)
    {
        StartCoroutine(GoDown(chick));
    }

    private IEnumerator GoDown(ChickenAnimationManager chick)
    {
        moveDown = true;
        yield return new WaitForSeconds(1f);
        moveDown = false;
        StartCoroutine(GoUp());
        chick.gameObject.SetActive(false);
        chickChild.SetActive(true);
    }

    private IEnumerator GoUp()
    {
        moveUp = true;
        yield return new WaitForSeconds(0.5f);
        moveUp = false;
    }
}
