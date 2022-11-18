using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Adventure
{


    public class SpearTrapBehaviourScript : MonoBehaviour
    {

        [SerializeField] private AudioSource _auds; //компонент audiosource
        [Header("Настройка ловушки")]
        [SerializeField] private float _closureTime = 5f; //время закрытия ловушки
        [SerializeField] private float _rechargeTime = 5f; //время перзарядки ловушки

        private bool _isActive = true;
        private bool _isRecharge = true;
        private bool _isDeactive = false;

        void Start()
        {


        }


        void Update()
        {
            //закрытие ловушки
            if (!_isActive)
            {
                Debug.Log("Ловушка сработала");

                _closureTime -= Time.deltaTime;//отнимаем время таймера
                if (_closureTime <= 0)
                {
                    Debug.Log("Ловушка закрылась");
                    //если время меньше или равно 0
                    this.transform.Translate(Vector3.up * -2f); //закрываем ловушку
                    _isActive = true;
                    _isRecharge = true;
                }
            }
            else if (_isRecharge)
            {
                _rechargeTime -= Time.deltaTime;//отнимаем время таймера
                if (_rechargeTime <= 0)
                {
                    Debug.Log("Ловушка заряжена");
                    //если время меньше или равно 0           
                    _isRecharge = false;
                }

            }





        }


        void OnTriggerEnter(Collider other)
        {
            if (!_isDeactive)
            {
                if (!_isRecharge && (other.name == "Player" || other.name == "RockСast(Clone)" || other.name == "Rat(Clone)"))
                {
                    _auds.Play();
                    this.transform.Translate(Vector3.up * 1f);
                    _rechargeTime = 5f;
                    _closureTime = 5f;
                    _isActive = false;
                    Destroy(other.gameObject);

                }
            }
        }

        internal void Activate()
        {
            _isDeactive = false;
        }

        internal void Deactivate()
        {
            _isDeactive = true;
        }


        ////закрытие ловушки
        //void OnTriggerExit(Collider other)
        //{
        //    //проверка на игрока или камень
        //    if (other.name == "Player" || other.name == "RockСast(Clone)")
        //    {

        //        _closureTime -= Time.deltaTime;//отнимаем время таймера
        //        if (_closureTime <= 0)
        //        { //если время меньше или равно 0
        //            this.transform.Translate(Vector3.up * -1f); //закрываем ловушку
        //        }

        //    }
        //}

    }
}
