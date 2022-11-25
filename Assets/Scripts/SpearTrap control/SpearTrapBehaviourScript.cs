using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Adventure
{


    public class SpearTrapBehaviourScript : MonoBehaviour
    {
        [SerializeField] private PlayerBehaviourScript _playerScript; //������ �� �������� ������ ������
        [SerializeField] private AudioSource _auds; //��������� audiosource
        [Header("��������� �������")]
        [SerializeField] private float _closureTime = 5f; //����� �������� �������
        [SerializeField] private float _rechargeTime = 5f; //����� ���������� �������

        private bool _isActive = true;
        private bool _isRecharge = true;
        public bool isDeactive = false;

        void Update()
        {
            //�������� �������
            if (!_isActive)
            {
                Debug.Log("������� ���������");

                _closureTime -= Time.deltaTime;//�������� ����� �������
                if (_closureTime <= 0)
                {
                    Debug.Log("������� ���������");
                    //���� ����� ������ ��� ����� 0
                    this.transform.Translate(Vector3.up * -2f); //��������� �������
                    _isActive = true;
                    _isRecharge = true;
                }
            }
            else if (_isRecharge)
            {
                _rechargeTime -= Time.deltaTime;//�������� ����� �������
                if (_rechargeTime <= 0)
                {
                    Debug.Log("������� ��������");
                    //���� ����� ������ ��� ����� 0           
                    _isRecharge = false;
                }

            }

        }


        void OnTriggerEnter(Collider other)
        {
            if (!isDeactive)
            {
                if (!_isRecharge && (other.name == "Player" || other.name == "Rock�ast(Clone)" || other.name == "Rat(Clone)"))
                {
                    _auds.Play();
                    this.transform.Translate(Vector3.up * 1f);
                    _rechargeTime = 5f;
                    _closureTime = 5f;
                    _isActive = false;
                    if(other.name == "Player")
                    {
                        _playerScript.Death();
                    }
                    else
                    {
                        Destroy(other.gameObject);
                    }
                    

                }
            }
        }

        internal void Activate()
        {
            isDeactive = false;
        }

        internal void Deactivate()
        {
            isDeactive = true;
        }


        ////�������� �������
        //void OnTriggerExit(Collider other)
        //{
        //    //�������� �� ������ ��� ������
        //    if (other.name == "Player" || other.name == "Rock�ast(Clone)")
        //    {

        //        _closureTime -= Time.deltaTime;//�������� ����� �������
        //        if (_closureTime <= 0)
        //        { //���� ����� ������ ��� ����� 0
        //            this.transform.Translate(Vector3.up * -1f); //��������� �������
        //        }

        //    }
        //}

    }
}
