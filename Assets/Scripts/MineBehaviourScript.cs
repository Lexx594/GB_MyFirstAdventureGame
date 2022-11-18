using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBehaviourScript : MonoBehaviour
{

    [SerializeField] private AudioSource _audsActive; //��������� audiosource
    [SerializeField] private AudioSource _audsExplosion; //��������� audiosource
    [Header("��������� ����")]
    [SerializeField] private float _ActiveTime = 2f; //�������� ������ ����
    //[SerializeField] private float _ExplosionTime = 3f; //����� ������ ����

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
            _ActiveTime -= Time.deltaTime;//�������� ����� �������
            if (_ActiveTime <= 0)
            {
                this.gameObject.transform.GetChild(0).gameObject.SetActive(false); //������������ 1 ������� (������ ����)
                this.gameObject.transform.GetChild(1).gameObject.SetActive(true);  //���������� 1 ������� (������ ������)
                _audsExplosion.Play();
                Debug.Log("����� ����");                             
                _isActive = false;
                _isExplosion = true;
                Destroy(this.gameObject, 3f); //���������� ������ ���� ����� 3 �������
            }
                       
        }
      

    }
    void OnTriggerStay(Collider other)
    {
        //���������� ������ ����������� � ���� ��������� ����
        if (_isExplosion)
        {
            Destroy(other.gameObject);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player" || collision.gameObject.name == "Rock�ast(Clone)")
        {
            _audsActive.Play();
            _isActive = true;
        }


    }



}

