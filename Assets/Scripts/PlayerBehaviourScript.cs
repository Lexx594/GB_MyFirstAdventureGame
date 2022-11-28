using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Adventure
{
    [RequireComponent(typeof(Animator))]
    public class PlayerBehaviourScript : MonoBehaviour
    {
        [Header("��������� ��������")]
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _runSpeed = 10f;
        [SerializeField] private float _rotateSpeed = 75f;
        [SerializeField] private float _jumpForce = 10f;

        private float _slowSpeed = 1f;

        private const string _horizontal = "Horizontal";
        private const string _vertical = "Vertical";
        private const string _running = "Running";

        [Header("�������� ������� �����")]
        [SerializeField] private float _playerHeight = 5f;
        [SerializeField] private LayerMask _whatIsGround;
        private bool _grounded;

        [Header("������ �����")]
        [SerializeField] private Transform _rockperf; //������ �����
        [SerializeField] private Transform _spaunPoint; //����� ������


        [Header("���������")]

        [SerializeField] private GameObject _stoneIcon;
        [SerializeField] private GameObject _slowStoneIcon;
        [SerializeField] private GameObject _fireStoneIcon;

        [SerializeField] private int _stone = 0;
        [SerializeField] private int _slowStone = 0;
        [SerializeField] private int _fireStone = 0;

        //private string _stoneText;
        //private string _slowStoneText;
        //private string _fireStoneText;


        private bool _action;
        private bool _attack;

        private float _vInput;
        private float _hInput;
        private bool _isRunning;


        private Animator _animator;
        private Rigidbody _rb;

        AudioSource m_AudioSource;

        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            m_AudioSource = GetComponent<AudioSource>();
            _animator = GetComponent<Animator>();
        }

        void Update()
        {

            // �������� ��������� �� �������� �� �����
            _grounded = Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _whatIsGround);

            _isRunning = Input.GetButton(_running);
            _vInput = Input.GetAxis(_vertical) * ((_isRunning ? _runSpeed : _moveSpeed) * _slowSpeed);
            _hInput = Input.GetAxis(_horizontal) * _rotateSpeed;

            // ��������� ������
            if (Input.GetKeyDown(KeyCode.Space) && _grounded)
            {
                _rb.AddForce(Vector3.up * _jumpForce, ForceMode.VelocityChange);
                Vector3 force = transform.forward * _vInput * _jumpForce * 60f;
                _rb.AddForce(force);
                //_rb.AddForce(Vector3.transform.up * _vInput, ForceMode.VelocityChange);
            }

            _animator.SetBool("Action", _action);
            _animator.SetBool("Attack", _attack);

            

            //��������� ���������
            if (_stone > 0)
            {
                _stoneIcon.SetActive(true);
                _stoneIcon.transform.GetChild(0).GetComponent<Text>().text = _stone.ToString();
                //Transform stoneText = _stoneIcon.transform.GetChild(0);
                //Text txt = stoneText.GetComponent<Text>();
                //txt.text = _stone.ToString();
            }
            else
            {
                _stoneIcon.SetActive(false);
            }
            if (_slowStone > 0)
            {
                _slowStoneIcon.SetActive(true);
                _slowStoneIcon.transform.GetChild(0).GetComponent<Text>().text = _slowStone.ToString();
            }
            else
            {
                _slowStoneIcon.SetActive(false);
            }
            if (_fireStone > 0)
            {
                _fireStoneIcon.SetActive(true);
                _fireStoneIcon.transform.GetChild(0).GetComponent<Text>().text = _fireStone.ToString();
            }
            else
            {
                _fireStoneIcon.SetActive(false);
            }


        }


        void FixedUpdate()
        {
            //������� ����� ���������� Vector3 ��� �������� �������� ����� � ������
            Vector3 rotation1 = Vector3.up * _hInput;
            //����� Quaternion.Euler ��������� �� ���� Vector3 � ���������� �������� �������� � ����� ������
            Quaternion angleRot = Quaternion.Euler(rotation1 * Time.fixedDeltaTime);

            _animator.SetBool("Grounded", _grounded);
            _animator.SetInteger("ForwardSpeed", (int)_vInput);

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

            //������ ����� �� ����������� �������

            RaycastHit hit; //���� ���� � ������������

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // ������� ��� �� ������ � ����������� ������� ����
            Debug.DrawRay(ray.origin, ray.direction * 5f, Color.green); //������������� ��� ������� ������

            if (Input.GetMouseButtonDown(0) && _stone != 0) // ���� ������ ����� ������ ���� � � ��� ���� �����
            {
                if(Physics.Raycast(ray, out hit)) //���� ��� ����� � �����������
                {                           
                    Vector3 direction = hit.point - transform.position; //���� ����� ������ ���� ����� ��� � ������������ ������
                    Quaternion rotation = Quaternion.LookRotation(direction); //����������� ���������� �� ������� ���������� �����������
                    rotation.x = 0f; //������������� ���������� �� ���� x � z
                    rotation.z = 0f;
                    //rotation = rotation.normalized; //����������� ���������� (��� ����� ������ ������ ��� ������������)
                    //_rb.MoveRotation(rotation); //������������ ���������

                    transform.localRotation = Quaternion.Slerp(transform.rotation, rotation, 50 * Time.deltaTime);
                    
                    _attack = true;
                    _stone--;
                    
                    Invoke(nameof(Attack), 0.5f);
                    Invoke(nameof(ResetAll), 0.5f);
                }
            }
        }

        private void Attack()
        {
            Instantiate(_rockperf, _spaunPoint.position, _spaunPoint.rotation); //������� ������ � ����� ����� � ��� ���������
        }

        private void ResetAll()
        {
            _action = false;
            _attack = false;
        }

        public void Death()
        {
            _animator.SetTrigger("Death");
            StartCoroutine(DeathPlayer());
        }

        private IEnumerator DeathPlayer()
        {
            yield return new WaitForSeconds(3f); 
            SceneManager.LoadScene(0);
            yield return null;
        }



        void OnTriggerStay(Collider other)
        {

            if (other.name == "SlowField")
            {
                _slowSpeed = 0.5f;
            }

            if (other.name == "StackRocks" && Input.GetMouseButtonDown(1))
            {
                _stone++;
                Debug.Log("������ ��������");
                _action = true;
                Invoke(nameof(ResetAll), 0.5f);
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.name == "SlowField")
            {
                _slowSpeed = 1f;
            }
        }

    }

}
