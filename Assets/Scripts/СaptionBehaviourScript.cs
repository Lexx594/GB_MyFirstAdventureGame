using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Adventure
{

    public class СaptionBehaviourScript : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        private void OnCollisionExit(Collision collision)
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }


    }

}

