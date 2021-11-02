using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCondor : MonoBehaviour
{
    public bool moveForward;
    public bool moveDown;
    public bool moveUp;

    public float forwardSpeed = 1f;
    public float downSpeed = 2f;
    public float upSpeed = 4f;

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
        chick.transform.parent = this.transform;
        chick.ChangeToScare();
    }

    private IEnumerator GoUp()
    {
        moveUp = true;
        yield return new WaitForSeconds(0.5f);
        moveUp = false;
    }
}
