using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages chicken animation
/// By Iman IRAJ DOOST and Ahmad JREDA
/// </summary>
public class ChickenAnimationManager : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private float waitTime = 0;

    private bool shouldMoveUp = false;
    public float upSpeed = 2f;
    public bool eatAnim = true;
    public bool flyAnim = false;
    public ParticleSystem par;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if(eatAnim)
            StartCoroutine(EnableEatAfter(waitTime));
        if (flyAnim)
            StartCoroutine(EnableFlyAfter(waitTime));
    }

    private IEnumerator EnableEatAfter(float t)
    {
        yield return new WaitForSeconds(t);
        anim.SetBool("Eat", true);
    }

    private IEnumerator EnableFlyAfter(float t)
    {
        yield return new WaitForSeconds(t);
        ChangeToFly();
    }

    public void ChangeToFly()
    {
        anim.SetBool("Fly", true);
    }

    public void ChangeToScare()
    {
        anim.SetBool("Scared", true);
    }

    public void ChangeToSurprise()
    {
        if (par != null)
            par.Play();
        anim.SetTrigger("Surprise");
    }

    public void FlyTowardsCondor()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 270, transform.eulerAngles.z);
        StartCoroutine(StartMovingUp());
    }

    private IEnumerator StartMovingUp()
    {
        shouldMoveUp = true;
        yield return new WaitForSeconds(2);
        shouldMoveUp = false;
    }

    private void Update()
    {
        if (shouldMoveUp)
            transform.Translate(Vector3.up * upSpeed * Time.deltaTime);
    }
}
