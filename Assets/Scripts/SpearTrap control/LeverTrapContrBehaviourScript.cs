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

        private void OnTriggerStay(Collider other)
        {
            //Debug.Log("выключатель ловушки");
            if (other.name == "Player" && Input.GetMouseButtonDown(1))
            {
                StartCoroutine(Active());                
            }
        }

        IEnumerator Active()
        {
            yield return new WaitForSeconds(1f);
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
        }
        
                
        


    }
}
