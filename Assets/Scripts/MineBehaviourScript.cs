using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBehaviourScript : MonoBehaviour
{

    [SerializeField] private AudioSource _audsActive; //компонент audiosource
    [SerializeField] private AudioSource _audsBoom; //компонент audiosource
    [Header("Настройка мины")]
    [SerializeField] private float _ActiveTime = 3f; //задержка взрыва мины
    [SerializeField] private float _BoomTime = 3f; //время взрыва мины

    private bool _isActive = false;
    private bool _isBoom = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_isActive)
        {
            //_audsZoom.Play();
            _ActiveTime -= Time.deltaTime;//отнимаем время таймера
            if (_ActiveTime <= 0)
            {
                
                _audsBoom.Play();
                Debug.Log("Взрыв мины");                             
                _isActive = false;
                _isBoom = true;
                //Destroy(gameObject, 3f);
            }
                       
        }
        else if(_isBoom)
        {
            _BoomTime -= Time.deltaTime;//отнимаем время таймера
            if (_BoomTime <= 0)
            {

                _audsBoom.Play();
                Debug.Log("Взрыв мины");
                //если время меньше или равно 0                
                _isActive = false;
                Destroy(gameObject);
                //GameObject.Destroy(this.gameObject);
            }



        }

    }
    void OnTriggerEnter(Collider other)
    {

        if (other.name == "Player" || other.name == "RockСast(Clone)")
        {

            //_audsBoom.Play();
            _audsActive.Play();
            _isActive = true;
        }
    }





}

