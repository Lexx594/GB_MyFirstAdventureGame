using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Adventure
{

    public class LeverTrapContrBehaviourScript : MonoBehaviour
    {
        [SerializeField] private AudioSource _pushButton; //компонент audiosource
        [SerializeField] private SpearTrapBehaviourScript _controledTrap;
        private bool _isActive = true;
        private bool _ready = true;
        private Animator _animator;


        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnTriggerStay(Collider other)
        {
            //Debug.Log("выключатель ловушки");
            if (other.name == "Player" && Input.GetMouseButtonDown(1))
            {
                //StartCoroutine(Active());
                if(_ready)
                {
                    Active();
                    _ready = false;
                    Invoke(nameof(ResetActive), 1f);
                }
            }
        }
        private void ResetActive() //сброс атаки
        {
            _ready = true;
        }

        private void Active()
        {

            if (!_isActive)
            {
                _pushButton.Play();
                _controledTrap.Activate();
                _isActive = true;
                Debug.Log("Ловушка включена");
            }
            else
            {
                _pushButton.Play();
                _controledTrap.Deactivate();
                _isActive = false;
                Debug.Log("Ловушка выключена");
            }
            _animator.SetBool("LeverDown", !_isActive);            

        }



        //IEnumerator Active()
        //{
        //    yield return new WaitForSeconds(1f);
        //    if (!_isActive)
        //    {
        //        _pushButton.Play();
        //        _controledTrap.Activate();
        //        _isActive = true;
        //        Debug.Log("Ловушка включена");
        //    }                   
        //    else
        //    {
        //        _pushButton.Play();
        //        _controledTrap.Deactivate();
        //        _isActive = false;
        //        Debug.Log("Ловушка выключена");
        //    }
        //    _animator.SetBool("LeverDown", _controledTrap.isDeactive);
        //}
        
                
        


    }
}
