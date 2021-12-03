using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEggManager : MonoBehaviour
{
    private bool hasClicked = false;
    public GameObject target;
    public float speed = 1f;

    public Camera cam;

    private void Update()
    {
        if (Input.GetMouseButton(0) && !hasClicked)
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if(hit.transform.CompareTag("EasterEgg"))
                    hasClicked = true;
            }
        }

        if (hasClicked)
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
}
