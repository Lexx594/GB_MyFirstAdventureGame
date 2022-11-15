using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockThrowBehaviourScript : MonoBehaviour
{
    public Transform rockperf; //Перфаб камня
    public Transform SpaunPoint; //Точка спауна

    [Header("Инвентарь")]

    [SerializeField] private float _rocks = 0f;



    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _rocks != 0f)
        {
            _rocks--;
            // Если нажали левую кнопку мыши
            Instantiate(rockperf, SpaunPoint.position, SpaunPoint.rotation); //создаем камень в точке спуна с его позициями
        }



    }

    void OnTriggerStay(Collider other)
    {
        
        if (other.name == "StackRocks" && Input.GetMouseButtonDown(1))
        {

            _rocks++;
            Debug.Log("Камень подобран");


            //Debug.Log("Вы возле кучи камней");

            //if (Input.GetMouseButtonDown(1))
            //{

            //}    
                
        }
    }


}
