using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Adventure
{
    public class BossAttackTriger : MonoBehaviour
    {
        [SerializeField] private BossScript _bossScript;

        void OnTriggerEnter(Collider other)
        {

            if (other.name == "Player")
            {
                _bossScript.playerInZone = true;
            }
        }

        void OnTriggerExit(Collider other)
        {

            if (other.name == "Player")
            {
                _bossScript.playerInZone = false;
            }
        }

    }
}
