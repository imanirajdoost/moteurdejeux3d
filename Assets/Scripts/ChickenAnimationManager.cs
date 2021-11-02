using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAnimationManager : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private float waitTime = 0;

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
}
