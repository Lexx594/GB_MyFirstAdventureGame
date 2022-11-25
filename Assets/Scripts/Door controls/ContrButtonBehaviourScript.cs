using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Adventure
{

    public class ContrButtonBehaviourScript : MonoBehaviour
    {

  
        [SerializeField] private ContrDoorBehaviourScript _controledDoor;
        [SerializeField] private AudioSource _pushButton; //компонент audiosource
        private Animator _animator;
        private bool _check = true;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter(Collider other)
        {
            
            if (other.name == "Player" && _check)
            {
                //Debug.Log("Кнопка нажата");
                _controledDoor.Activate();
                _pushButton.Play();
                _animator.SetTrigger("ButtonPress");
                _check = false;
                //this.gameObject.GetComponent<Animator>().enabled = false;
            }         
            
        }

        //private void OnTriggerExit(Collider other)
        //{

        //    if (other.name == "Player")
        //    {
        //        Debug.Log("Кнопка отпущена");
        //        _controledDoor.Deactivate();
        //    }

        //}



    }


}
