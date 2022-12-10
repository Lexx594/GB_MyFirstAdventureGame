using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Adventure
{
    public class WinnerScript : MonoBehaviour
    {

        [SerializeField] private GameObject _panelWinner;

        public bool activePortal;
        void OnTriggerEnter(Collider other)
        {


            if (other.name == "Player" && activePortal)
            {

                _panelWinner.SetActive(true);
                //Time.timeScale = 0f;
                AudioListener.volume = 0f;


            }


        }
    }
}
