using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Adventure
{

    public class ContrButtonBehaviourScript : MonoBehaviour
    {

  
        [SerializeField] private ContrDoorBehaviourScript _controledDoor;
        [SerializeField] private AudioSource _pushButton; //��������� audiosource


        private void OnTriggerEnter(Collider other)
        {
            
            if (other.name == "Player")
            {
                //Debug.Log("������ ������");
                _controledDoor.Activate();
                _pushButton.Play();
            }         
            
        }

        //private void OnTriggerExit(Collider other)
        //{

        //    if (other.name == "Player")
        //    {
        //        Debug.Log("������ ��������");
        //        _controledDoor.Deactivate();
        //    }

        //}



    }


}
