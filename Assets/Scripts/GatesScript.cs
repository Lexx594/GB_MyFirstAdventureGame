using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatesScript : MonoBehaviour
{
    [SerializeField] private AudioSource _openGate;
    [SerializeField] private AudioSource _hitAxe;


    void OnTriggerEnter(Collider other)
    {
        if (other.name == "AxeWork")
        {
            
            
            gameObject.GetComponent<Animator>().enabled = true;
            Invoke(nameof(PlaySourse), 1f);
            _hitAxe.Play();
        }
    }

    private void PlaySourse()
    {
        _openGate.Play();
    }


}
