using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Adventure
{

    public class LeverRight : MonoBehaviour
    {

        [SerializeField] private AudioSource _pushButton;
        [SerializeField] private OpenDoor _controledDoor;
        private bool _isActive = true;
        private bool _ready = true;
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }
        private void OnTriggerStay(Collider other)
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

            if (!_isActive)
            {
                _pushButton.Play();
                _controledDoor.lewerRight = true;
                _isActive = true;
            }
            else
            {
                _pushButton.Play();
                _controledDoor.lewerRight = false;
                _isActive = false;
            }
            _animator.SetBool("LeverDown", !_isActive);
        }
    }
}
