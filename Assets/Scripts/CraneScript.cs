using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Adventure
{
    public class CraneScript : MonoBehaviour
    {
        [SerializeField] private GameObject _particleEffect1;
        [SerializeField] private GameObject _particleEffect2;
        [SerializeField] private AudioSource _audsCrane;
        private bool _active = false;
        private bool _ready = true;

        void OnTriggerStay(Collider other)
        {


            if (other.name == "Player" && Input.GetMouseButtonDown(1))
            {
                if (_ready)
                {
                    Active();
                    _ready = false;
                    Invoke(nameof(ResetActive), 1f);
                }
            }
        }
        private void ResetActive()
        {
            _ready = true;
        }
        private void Active()
        {
            if (!_active)
            {
                _audsCrane.Play();
                _particleEffect1.SetActive(true);
                _particleEffect2.SetActive(true);
                _active = true;
            }
            else
            {
                _audsCrane.Play();
                _particleEffect1.SetActive(false);
                _particleEffect2.SetActive(false);
                _active = false;
            }
        }
    }
}
