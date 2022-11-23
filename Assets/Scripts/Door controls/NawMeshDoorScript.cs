using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NawMeshDoorScript : MonoBehaviour
{
    [SerializeField] private AudioSource _auds;
    private bool _isAcnive = false;

    void Start()
    {      
        Close_open();
    }

    private void Close_open()
    {
        if (!_isAcnive)
        {
            this.transform.Translate(Vector3.left * 1.5f);
            _isAcnive = true;
            _auds.Play();
        }
        else
        { 
            this.transform.Translate(Vector3.right * 1.5f);
            _isAcnive = false;
            _auds.Play();
        }
        Invoke(nameof(Close_open), 30f);
               
    }       
}
