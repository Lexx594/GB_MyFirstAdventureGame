using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Adventure
{

    public class ContrDoorBehaviourScript : MonoBehaviour
    {

        [SerializeField] private Transform _door;
        [SerializeField] private Vector3 _openedDoor = new Vector3(0f, 90f, 0f);
        [SerializeField] private Vector3 _closedDoor = new Vector3(0f, 0f, 0f);
        [SerializeField] private float speed = 1f;

        private Quaternion _openedDoorQ;
        private Quaternion _closedDoorQ;
        
        private const float Delta = 5f;


        private void Awake()
        {
            _openedDoorQ = Quaternion.Euler(_openedDoor);
            _closedDoorQ = Quaternion.Euler(_closedDoor);

        }

        public void Activate()
        {
            StartCoroutine(OpenDoor());

        }

        private IEnumerator OpenDoor()
        {
            Debug.Log("Открытие начато");
            while (Quaternion.Angle(_door.rotation, _openedDoorQ) > Delta)
            {
                Debug.Log("мы в цикле");
                _door.rotation = Quaternion.Slerp(_door.rotation, _openedDoorQ, speed *Time.deltaTime);
                yield return new WaitForEndOfFrame();

            }


            yield return null;
        }

        //public void Deactivate()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
