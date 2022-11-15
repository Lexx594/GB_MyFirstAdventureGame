using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBehaviourScript : MonoBehaviour
{

    [SerializeField] private AudioSource _audsActive; //��������� audiosource
    [SerializeField] private AudioSource _audsBoom; //��������� audiosource
    [Header("��������� ����")]
    [SerializeField] private float _ActiveTime = 3f; //�������� ������ ����
    [SerializeField] private float _BoomTime = 3f; //����� ������ ����

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
            _ActiveTime -= Time.deltaTime;//�������� ����� �������
            if (_ActiveTime <= 0)
            {
                
                _audsBoom.Play();
                Debug.Log("����� ����");                             
                _isActive = false;
                _isBoom = true;
                //Destroy(gameObject, 3f);
            }
                       
        }
        else if(_isBoom)
        {
            _BoomTime -= Time.deltaTime;//�������� ����� �������
            if (_BoomTime <= 0)
            {

                _audsBoom.Play();
                Debug.Log("����� ����");
                //���� ����� ������ ��� ����� 0                
                _isActive = false;
                Destroy(gameObject);
                //GameObject.Destroy(this.gameObject);
            }



        }

    }
    void OnTriggerEnter(Collider other)
    {

        if (other.name == "Player" || other.name == "Rock�ast(Clone)")
        {

            //_audsBoom.Play();
            _audsActive.Play();
            _isActive = true;
        }
    }





}

