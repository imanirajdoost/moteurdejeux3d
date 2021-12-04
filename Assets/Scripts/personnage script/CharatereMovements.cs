using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Manages chicken mouvement with camera 
/// By Ahmad JREDA
/// </summary>
public class CharatereMovements : MonoBehaviour
{
    public float speed = 12f;
    public int skeed = 3;
    public double Angle = 0;
    public float mH=0;
    public float mV=0;
    public bool est_mort = false;
    public bool Toucher_Condor = false;
    public Rigidbody rb;
    private bool firsttime = true;

    private void Awake()
    {

    }

    void Update()
    {
        if (!est_mort)
        {
            //récupère les mouvement en qwerty du joueur 
            mH = Input.GetAxis("Horizontal");
            mV = Input.GetAxis("Vertical");
            //faire bouger le joueur selon les inputes (haut/bas , gauche/droite et vecteur constant vers l'avant ) 
             rb.velocity = new Vector3(Time.deltaTime *speed*-100, mV * Time.deltaTime*100* speed, mH * Time.deltaTime*100* speed);
  
        }
        else
        {
            //on ne bouge plus quand on est mort ^^
            rb.velocity = new Vector3(0, 0, 0);
        }
    }
}
