using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Adventure
{
    public class BossKillScript : MonoBehaviour
    {
        [SerializeField] private BossScript _bossScript;

        [SerializeField] private GameObject _particleEffectExp1;
        [SerializeField] private GameObject _particleEffectExp2;
        [SerializeField] private GameObject _particleEffect1;
        [SerializeField] private GameObject _particleEffect2;
        [SerializeField] private GameObject _boss;
        private bool _active = true;

        void OnTriggerEnter(Collider other)
        {
            if (other.name == "LavaRock—ast Variant(Clone)" && _active)
            {
                _particleEffectExp1.SetActive(true);
                _particleEffectExp2.SetActive(true);
                _particleEffect1.SetActive(false);
                _particleEffect2.SetActive(false);
                _active = false;
                _bossScript.Death();
            }
        }
    }
}
