using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Adventure
{
    public class PortalTerminalScript : MonoBehaviour
    {
        [SerializeField] private WinnerScript _winnerScript;

        [SerializeField] private GameObject _portalCentr;
        [SerializeField] private AudioSource _audsTerminal;
        [SerializeField] private AudioSource _audsPortal;

        void OnTriggerStay(Collider other)
        {


            if (other.name == "Player" && Input.GetMouseButtonDown(1))
            {
                _portalCentr.SetActive(true);
                _audsTerminal.Play();
                _audsPortal.Play();
                _winnerScript.activePortal = true;
            }


        }
    }
}
