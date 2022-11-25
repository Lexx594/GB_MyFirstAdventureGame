using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Adventure
{


    public class SpearTrapBehaviourScript : MonoBehaviour
    {
        [SerializeField] private PlayerBehaviourScript _playerScript; //ссылка на основной скрипт игрока
        [SerializeField] private AudioSource _auds; //компонент audiosource
        [Header("Настройка ловушки")]
        [SerializeField] private float _closureTime = 5f; //время закрытия ловушки
        [SerializeField] private float _rechargeTime = 5f; //время перзарядки ловушки

        private bool _isActive = true;
        private bool _isRecharge = true;
        public bool isDeactive = false;

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
            if (!isDeactive)
            {
                if (!_isRecharge && (other.name == "Player" || other.name == "RockСast(Clone)" || other.name == "Rat(Clone)"))
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
