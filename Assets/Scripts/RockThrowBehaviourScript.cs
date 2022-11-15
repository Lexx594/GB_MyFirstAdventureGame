using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockThrowBehaviourScript : MonoBehaviour
{
    public Transform rockperf; //������ �����
    public Transform SpaunPoint; //����� ������

    [Header("���������")]

    [SerializeField] private float _rocks = 0f;



    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _rocks != 0f)
        {
            _rocks--;
            // ���� ������ ����� ������ ����
            Instantiate(rockperf, SpaunPoint.position, SpaunPoint.rotation); //������� ������ � ����� ����� � ��� ���������
        }



    }

    void OnTriggerStay(Collider other)
    {
        
        if (other.name == "StackRocks" && Input.GetMouseButtonDown(1))
        {

            _rocks++;
            Debug.Log("������ ��������");


            //Debug.Log("�� ����� ���� ������");

            //if (Input.GetMouseButtonDown(1))
            //{

            //}    
                
        }
    }


}
