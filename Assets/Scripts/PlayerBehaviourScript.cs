using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviourScript : MonoBehaviour
{
    [Header("��������� ��������")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _runSpeed = 10f;
    [SerializeField] private float _rotateSpeed = 75f;
    [SerializeField] private float _jumpForce = 10f;

    private float slowSpeed = 1f;

    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";
    private const string _running = "Running";


    [Header("�������� ������� �����")]
    [SerializeField] private float _playerHeight = 5f;
    [SerializeField] private LayerMask _whatIsGround;
    private bool _grounded;


    private float _vInput;
    private float _hInput;
    private bool _isRunning;

    private Rigidbody _rb;

    AudioSource m_AudioSource;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
    }
      
    void Update()
    {       
        
        // �������� ��������� �� �������� �� �����
        _grounded = Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _whatIsGround);

        _isRunning = Input.GetButton(_running);
        _vInput = Input.GetAxis(_vertical) * ((_isRunning ? _runSpeed : _moveSpeed) * slowSpeed) ;        
        _hInput = Input.GetAxis(_horizontal) * _rotateSpeed;
        
        // ��������� ������
        if (Input.GetKeyDown(KeyCode.Space) && _grounded)
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.VelocityChange);
            Vector3 force = transform.forward * _vInput * _jumpForce *60f;
            _rb.AddForce(force);
            //_rb.AddForce(Vector3.transform.up * _vInput, ForceMode.VelocityChange);
        }
    }


    void FixedUpdate()
    {
        //������� ����� ���������� Vector3 ��� �������� �������� ����� � ������
        Vector3 rotation = Vector3.up * _hInput;
        //����� Quaternion.Euler ��������� �� ���� Vector3 � ���������� �������� �������� � ����� ������
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);

        //����� ������ MovePosition �� ����� ���������� _rb, ������� ��������� �������� Vector3 � ��������������� ������� ������������ ����:
        if (_grounded)
        {
            _rb.MovePosition(transform.position + transform.forward * _vInput * Time.fixedDeltaTime);
        }
               
       


        _rb.MoveRotation(_rb.rotation * angleRot);

        //bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(_vInput, 0f);
        bool isWalking = hasVerticalInput;
        //m_Animator.SetBool("IsWalking", isWalking);

        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }




    }



    void OnTriggerStay(Collider other)
    {
        
        if (other.name == "SlowField")
        {
            slowSpeed = 0.5f;

            Debug.Log("�� �� ����������� ����");
        }
    }

    void OnTriggerExit(Collider other)
    {

        if (other.name == "SlowField")
        {
            slowSpeed = 1f;

            Debug.Log("�� ����� �� ������������ ����");
        }
    }



}
