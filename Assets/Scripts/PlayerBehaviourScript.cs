using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using TMPro;

namespace Adventure
{
    [RequireComponent(typeof(Animator))]
    public class PlayerBehaviourScript : MonoBehaviour
    {
        [Header("��������� ��������")]
        [SerializeField] private float _moveSpeed = 4f;
        [SerializeField] private float _runSpeed = 7f;
        [SerializeField] private float _rotateSpeed = 75f;
        //[SerializeField] private float _jumpForce = 10f;

        private float _slowSpeed = 1f;

        private const string _horizontal = "Horizontal";
        private const string _vertical = "Vertical";
        private const string _running = "Running";

        [Header("�������� ������� �����")]
        [SerializeField] private float _playerHeight = 5f;
        [SerializeField] private LayerMask _whatIsGround;
        private bool _grounded;

        [Header("������ �����")]
        [SerializeField] private Transform _rockpref; //������ �����
        [SerializeField] private Transform _slowStonepref;
        [SerializeField] private Transform _fireStonepref;
        [SerializeField] private Transform _spaunPoint; //����� ������


        [Header("���������")]

        [SerializeField] private GameObject _stoneIcon;
        [SerializeField] private GameObject _slowStoneIcon;
        [SerializeField] private GameObject _fireStoneIcon;
        [SerializeField] private GameObject _axeIcon;
        [SerializeField] private GameObject _bottleIcon;

        [SerializeField] private GameObject _stoneObject;
        [SerializeField] private GameObject _slowStoneObject;
        [SerializeField] private GameObject _fireStoneObject;
        [SerializeField] private GameObject _axeObject;
        [SerializeField] private GameObject _bottleObject;


        [SerializeField] private int _stone = 0;
        [SerializeField] private int _slowStone = 0;
        [SerializeField] private int _fireStone = 0;
        [SerializeField] private int _axe = 0;
        [SerializeField] private int _bottle = 0;

        //private string _stoneText;
        //private string _slowStoneText;
        //private string _fireStoneText;


        [SerializeField] private GameObject _box;
        [SerializeField] private GameObject _itemName;
        [SerializeField] private GameObject _panelDeath;
        private bool _action;
        private bool _attack;
        //private bool _isPushing;
        private bool _isStrong;
        private float _vInput;
        private float _hInput;
        private bool _isRunning;
        private bool _isDeath = false;
        private int _selectActiveThings = -1;

        private Animator _animator;
        private Rigidbody _rb;


        [Header("�����")]
        [SerializeField] private AudioSource _audsHit;
        [SerializeField] private AudioSource _audsDeath;
        [SerializeField] private AudioSource _audsBox;


        AudioSource m_AudioSource;

        private float _rechargeTime = 1f;
        private bool _playBoxSourse;

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
            // ������ �������� �.�. �� ������������ ������� ����
            //if (Input.GetKeyDown(KeyCode.Space) && _grounded)
            //{
                //_rb.AddForce(Vector3.up * _jumpForce, ForceMode.VelocityChange);
                //Vector3 force = transform.forward * _vInput * _jumpForce * 60f;
                //_rb.AddForce(force);
                
            //}

            _animator.SetBool("Action", _action);
            _animator.SetBool("Attack", _attack);


            _rechargeTime -= Time.deltaTime;//�������� ����� �������
            if (_rechargeTime <= 0)
            {
                if (_selectActiveThings == 0 && _stone != 0) _stoneObject.SetActive(true);
                else _stoneObject.SetActive(false);

                if (_selectActiveThings == 1 && _slowStone != 0) _slowStoneObject.SetActive(true);
                else _slowStoneObject.SetActive(false);

                if (_selectActiveThings == 2 && _fireStone != 0) _fireStoneObject.SetActive(true);
                else _fireStoneObject.SetActive(false);
            }
            
            
            if (_selectActiveThings == 3 && _axe != 0) _axeObject.SetActive(true);
            else _axeObject.SetActive(false);

            if (_selectActiveThings == 4 && _bottle != 0) _bottleObject.SetActive(true);
            else _bottleObject.SetActive(false);



            //��������� ���������
            if (_stone > 0)
            {
                _stoneIcon.SetActive(true);
                _stoneIcon.transform.GetChild(0).GetComponent<Text>().text = _stone.ToString();
                //_stoneIcon.transform.parent.transform.parent.transform.GetChild(3).transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                _stoneIcon.SetActive(false);
                _stoneIcon.transform.parent.transform.parent.transform.GetChild(3).transform.GetChild(0).gameObject.SetActive(false);


            }
            if (_slowStone > 0)
            {
                _slowStoneIcon.SetActive(true);
                _slowStoneIcon.transform.GetChild(0).GetComponent<Text>().text = _slowStone.ToString();
            }
            else
            {
                _slowStoneIcon.SetActive(false);
                _stoneIcon.transform.parent.transform.parent.transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(false);

            }
            if (_fireStone > 0)
            {
                _fireStoneIcon.SetActive(true);
                _fireStoneIcon.transform.GetChild(0).GetComponent<Text>().text = _fireStone.ToString();

            }
            else
            {
                _fireStoneIcon.SetActive(false);
                _stoneIcon.transform.parent.transform.parent.transform.GetChild(3).transform.GetChild(2).gameObject.SetActive(false);

            }

            if (_axe > 0)
            {
                _axeIcon.SetActive(true);
                _axeIcon.transform.GetChild(0).GetComponent<Text>().text = _axe.ToString();
            }
            else
            {
                _axeIcon.SetActive(false);
                _stoneIcon.transform.parent.transform.parent.transform.GetChild(3).transform.GetChild(3).gameObject.SetActive(false);

            }
            if (_bottle > 0)
            {
                _bottleIcon.SetActive(true);
                _bottleIcon.transform.GetChild(0).GetComponent<Text>().text = _bottle.ToString();
            }
            else
            {
                _bottleIcon.SetActive(false);
                _stoneIcon.transform.parent.transform.parent.transform.GetChild(3).transform.GetChild(4).gameObject.SetActive(false);

            }

            //����������� �������� �������
            if (Input.GetMouseButtonDown(2)) SelectActive();
        
            if(_playBoxSourse)
            {
                _audsBox.Play();
            }
            else
            {
                _audsBox.Stop();
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
            //��� �������� ���������
            //_animator.SetBool("Pushing", _isPushing);
            
            if (!_isDeath)
            {

                //����� ������ MovePosition �� ����� ���������� _rb, ������� ��������� �������� Vector3 � ��������������� ������� ������������ ����:
                if (_grounded)
                {
                    _rb.MovePosition(transform.position + transform.forward * _vInput * Time.fixedDeltaTime);
                }

                _rb.MoveRotation(_rb.rotation * angleRot);
            }
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

            //������ ����� �� ����������� ������� �������� �.�. � ������ ���� �� ����� ������������ � ������ ������

            //RaycastHit hit; //���� ���� � ������������

            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // ������� ��� �� ������ � ����������� ������� ����
            //Debug.DrawRay(ray.origin, ray.direction * 5f, Color.green); //������������� ��� ������� ������

            if (Input.GetMouseButtonDown(0) && _selectActiveThings != -1) // ���� ������ ����� ������ ���� � � ��� ���� �����
            {
                //if (Physics.Raycast(ray, out hit)) //���� ��� ����� � �����������
                //{
                    //Vector3 direction = hit.point - transform.position; //���� ����� ������ ���� ����� ��� � ������������ ������
                    //Quaternion rotation = Quaternion.LookRotation(direction); //����������� ���������� �� ������� ���������� �����������
                    //rotation.x = 0f; //������������� ���������� �� ���� x � z
                    //rotation.z = 0f;
                    //rotation = rotation.normalized; //����������� ���������� (��� ����� ������ ������ ��� ������������)
                    //_rb.MoveRotation(rotation); //������������ ���������

                    //transform.localRotation = Quaternion.Slerp(transform.rotation, rotation, 50 * Time.deltaTime);

                    //_attack = true;


                    if (_selectActiveThings >= 0 && _selectActiveThings < 4)
                    {
                        _animator.SetTrigger("AttackAxe");
                        _audsHit.Play();
                        Invoke(nameof(Attack), 0.5f);
                        Invoke(nameof(ResetAll), 0.5f);
                    }
                    else if (_selectActiveThings == 4) Drink();
                //}
            }
        }

        private void Attack()
        {
            if(_selectActiveThings == 0 && _stone !=0)
            {
                Instantiate(_rockpref, _spaunPoint.position, _spaunPoint.rotation); //������� ������ � ����� ����� � ��� ���������
                _stone--;
                _rechargeTime = 1.5f;
                _stoneObject.SetActive(false);
            }

            if (_selectActiveThings == 1 && _slowStone != 0)
            {
                Instantiate(_slowStonepref, _spaunPoint.position, _spaunPoint.rotation); //������� ������ � ����� ����� � ��� ���������
                _slowStone--;
                _rechargeTime = 1.5f;
                _slowStoneObject.SetActive(false);
            }

            if (_selectActiveThings == 2 && _fireStone != 0)
            {
                Instantiate(_fireStonepref, _spaunPoint.position, _spaunPoint.rotation); //������� ������ � ����� ����� � ��� ���������
                _fireStone--;
                _rechargeTime = 1.5f;
                _fireStoneObject.SetActive(false);
            }

        }
        private void Drink()
        {
            if (_selectActiveThings == 4 && _bottle != 0)
            {
                _animator.SetTrigger("Drink");
                _bottle--;
                _isStrong = true;
            }

        }





        private void ResetAll()
        {
            _action = false;
            _attack = false;
        }

        public void Death()
        {
            _audsDeath.Play();
            _isDeath = true;
            _animator.SetTrigger("Death");
            StartCoroutine(DeathPlayer());
        }

        private IEnumerator DeathPlayer()
        {



            yield return new WaitForSeconds(2f);
            //SceneManager.LoadScene(0);
            _panelDeath.SetActive(true);
            //Time.timeScale = 0f;
            AudioListener.volume = 0f;

            yield return null;
        }

        private void SelectActive()
        {
            if (_selectActiveThings == -1)
            {
                if (_stone > 0)
                {
                    ClearSelectActive();
                    _stoneIcon.transform.parent.transform.parent.transform.GetChild(3).transform.GetChild(0).gameObject.SetActive(true);
                    _selectActiveThings++;
                    _itemName.GetComponent<TextMeshProUGUI>().text = "������";
                    goto M1;
                    
                }
                else _selectActiveThings = 0;
            }
            if (_selectActiveThings == 0)
            {
                if (_slowStone > 0)
                {
                    ClearSelectActive();
                    _stoneIcon.transform.parent.transform.parent.transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(true);
                    _selectActiveThings++;
                    _itemName.GetComponent<TextMeshProUGUI>().text = "����������� ������";
                    goto M1;
                }
                else _selectActiveThings = 1;
            }

            if (_selectActiveThings == 1)
            {
                if (_fireStone > 0)
                {
                    ClearSelectActive();
                    _stoneIcon.transform.parent.transform.parent.transform.GetChild(3).transform.GetChild(2).gameObject.SetActive(true);
                    _selectActiveThings++;
                    _itemName.GetComponent<TextMeshProUGUI>().text = "�������� ������";
                    goto M1;
                }
                else _selectActiveThings = 2;
            }
            if (_selectActiveThings == 2)
            {
                if (_axe > 0)
                {
                    ClearSelectActive();
                    _stoneIcon.transform.parent.transform.parent.transform.GetChild(3).transform.GetChild(3).gameObject.SetActive(true);
                    _selectActiveThings++;
                    _itemName.GetComponent<TextMeshProUGUI>().text = "�����";
                    goto M1;
                }
                else _selectActiveThings = 3;
            }
            if (_selectActiveThings == 3)
            {
                if (_bottle > 0)
                {
                    ClearSelectActive();
                    _stoneIcon.transform.parent.transform.parent.transform.GetChild(3).transform.GetChild(4).gameObject.SetActive(true);
                    _selectActiveThings = 4;
                    _itemName.GetComponent<TextMeshProUGUI>().text = "����� ����";
                    goto M1;
                }
                else _selectActiveThings = 4;
            }

            if(_selectActiveThings == 4)
            {
                ClearSelectActive();
                _stoneIcon.transform.parent.transform.parent.transform.GetChild(3).transform.GetChild(5).gameObject.SetActive(true);
                _selectActiveThings = -1;
                _itemName.GetComponent<TextMeshProUGUI>().text = "�����";
            }


        M1:;



        }

        


        private void ClearSelectActive()
        {
            _stoneIcon.transform.parent.transform.parent.transform.GetChild(3).transform.GetChild(0).gameObject.SetActive(false);
            _stoneIcon.transform.parent.transform.parent.transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(false);
            _stoneIcon.transform.parent.transform.parent.transform.GetChild(3).transform.GetChild(2).gameObject.SetActive(false);
            _stoneIcon.transform.parent.transform.parent.transform.GetChild(3).transform.GetChild(3).gameObject.SetActive(false);
            _stoneIcon.transform.parent.transform.parent.transform.GetChild(3).transform.GetChild(4).gameObject.SetActive(false);
            _stoneIcon.transform.parent.transform.parent.transform.GetChild(3).transform.GetChild(5).gameObject.SetActive(false);
        }

        void OnTriggerStay(Collider other)
        {

            if (other.name == "SlowField")
            {
                _slowSpeed = 0.3f;
            }

            if (other.name == "PointTakeSlowStone" && Input.GetMouseButtonDown(1))
            {
                _stone--;
                _slowStone++;
                Debug.Log("������ ����������� ������");
                _action = true;
                Invoke(nameof(ResetAll), 0.5f);
            }

            if (other.name == "PointTakeFireStone" && Input.GetMouseButtonDown(1))
            {
                _stone--;
                _fireStone++;
                Debug.Log("������ �������� ������");
                _action = true;
                Invoke(nameof(ResetAll), 0.5f);
            }


            if (other.name == "Box" && Input.GetKey(KeyCode.E) && _isStrong)
            {
                //_isPushing = true;
                _moveSpeed = 1f;
                _box.GetComponent<Rigidbody>().isKinematic = false;
                _playBoxSourse = true;

            }
            else
            {
                //_isPushing = false;
                _moveSpeed = 4f;
                _box.GetComponent<Rigidbody>().isKinematic = true;
                _playBoxSourse = false;

            }




            if (other.tag == "Things" && Input.GetMouseButtonDown(1))
            {                                              
                if(other.name == "StackRocks")
                {
                    _stone++;
                    Debug.Log("������ ��������");
                    _action = true;
                    Invoke(nameof(ResetAll), 0.5f);
                }

                if (other.name == "Axe")
                {
                    _axe++;
                    Debug.Log("����� ��������");
                    Destroy(other.gameObject, 0.5f);
                    _action = true;
                    Invoke(nameof(ResetAll), 0.5f);
                }

                if (other.name == "Bottle")
                {
                    _bottle++;
                    Debug.Log("����� ���������");
                    Destroy(other.gameObject, 0.5f);
                    _action = true;
                    Invoke(nameof(ResetAll), 0.5f);
                }
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
