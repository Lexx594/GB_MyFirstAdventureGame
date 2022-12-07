using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatesScript : MonoBehaviour
{




    void OnTriggerEnter(Collider other)
    {
        if (other.name == "AxeWork")
        {
            gameObject.GetComponent<Animator>().enabled = true;
        }
    }




}
