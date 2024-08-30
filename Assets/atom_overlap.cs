using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atom_overlap : MonoBehaviour
{
    public Material m;
    public Material m2;

    private void OnTriggerEnter(Collider other)
    {
        this.GetComponent<Renderer>().material = m;
    }

    public void OnTriggerExit(Collider other)
    {
        if (m2 != null)
        {
            this.GetComponent<Renderer>().material = m2;
        }
    }

    /*
    public void OnCollisionEnter(Collision col)
    {
        print("reached collision enter");
    }

    void OnCollisionExit(Collision other)
    {
        print("reached collision exit");
        if (m2!=null)
        {
            this.GetComponent<Renderer>().material = m2;
        }
    }
    */
}
