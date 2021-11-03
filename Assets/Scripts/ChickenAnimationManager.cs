using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAnimationManager : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private float waitTime = 0;

    private bool shouldMoveUp = false;
    public float upSpeed = 2f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        StartCoroutine(EnableEatAfter(waitTime));
    }

    private IEnumerator EnableEatAfter(float t)
    {
        yield return new WaitForSeconds(t);
        anim.SetBool("Eat", true);
    }

    public void ChangeToScare()
    {
        anim.SetBool("Scared", true);
    }

    public void ChangeToSurprise()
    {
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
