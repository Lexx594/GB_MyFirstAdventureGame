using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBehaviourScript : MonoBehaviour
{

    [SerializeField] private AudioSource _audsActive; //компонент audiosource
    [SerializeField] private AudioSource _audsExplosion; //компонент audiosource
    [Header("Настройка мины")]
    [SerializeField] private float _ActiveTime = 2f; //задержка взрыва мины
    //[SerializeField] private float _ExplosionTime = 3f; //время взрыва мины

    private bool _isActive = false;
    private bool _isExplosion = false;

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
                this.gameObject.transform.GetChild(0).gameObject.SetActive(false); //деактивируем 1 ребенка (модель мины)
                this.gameObject.transform.GetChild(1).gameObject.SetActive(true);  //активируем 1 ребенка (эффект взрыва)
                _audsExplosion.Play();
                Debug.Log("Взрыв мины");                             
                _isActive = false;
                _isExplosion = true;
                Destroy(this.gameObject, 3f); //уничтожаем объект мина через 3 секунды
            }
                       
        }
      

    }
    void OnTriggerStay(Collider other)
    {
        //уничтожаем объект находящийся в зоне поражения мины
        if (_isExplosion)
        {
            Destroy(other.gameObject);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player" || collision.gameObject.name == "RockСast(Clone)")
        {
            _audsActive.Play();
            _isActive = true;
        }


    }



}

