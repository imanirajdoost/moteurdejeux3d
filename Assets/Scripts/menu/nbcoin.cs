using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class nbcoin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void addcoin()
    {
        GetComponent<Text>().text =""+GameManager.instance.nbcoin;
    }
    void Update()
    {
    }
}
