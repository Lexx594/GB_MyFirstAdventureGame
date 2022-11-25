using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Adventure
{
    [RequireComponent(typeof(Animator))]
    public class PlayerBehaviourScript : MonoBehaviour
    {
        [Header("Настройка движения")]
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _runSpeed = 10f;
        [SerializeField] private float _rotateSpeed = 75f;
        [SerializeField] private float _jumpForce = 10f;

        private float _slowSpeed = 1f;

        private const string _horizontal = "Horizontal";
        private const string _vertical = "Vertical";
        private const string _running = "Running";

        [Header("Проверка касания земли")]
        [SerializeField] private float _playerHeight = 5f;
        [SerializeField] private LayerMask _whatIsGround;
        private bool _grounded;

        [Header("Бросок камня")]
        [SerializeField] private Transform _rockperf; //Префаб камня
        [SerializeField] private Transform _spaunPoint; //Точка спауна


        [Header("Инвентарь")]

        [SerializeField] private float _rocks = 0f;


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

            // проверка находится ли персонаж на земле
            _grounded = Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _whatIsGround);

            _isRunning = Input.GetButton(_running);
            _vInput = Input.GetAxis(_vertical) * ((_isRunning ? _runSpeed : _moveSpeed) * _slowSpeed);
            _hInput = Input.GetAxis(_horizontal) * _rotateSpeed;

            // реализуем прыжок
            if (Input.GetKeyDown(KeyCode.Space) && _grounded)
            {
                _rb.AddForce(Vector3.up * _jumpForce, ForceMode.VelocityChange);
                Vector3 force = transform.forward * _vInput * _jumpForce * 60f;
                _rb.AddForce(force);
                //_rb.AddForce(Vector3.transform.up * _vInput, ForceMode.VelocityChange);
            }

            _animator.SetBool("Action", _action);
            _animator.SetBool("Attack", _attack);
            if (Input.GetMouseButtonDown(0) && _rocks != 0f)
            {
                _attack = true;
                _rocks--;
                // Если нажали левую кнопку мыши
                Invoke(nameof(Attack), 0.5f);
                Invoke(nameof(ResetAll), 0.5f);
            }

        }


        void FixedUpdate()
        {
            //Создаем новую переменную Vector3 для хранения вращения влево и вправо
            Vector3 rotation = Vector3.up * _hInput;
            //Метод Quaternion.Euler принимает на вход Vector3 и возвращает значение поворота в углах Эйлера
            Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);

            _animator.SetBool("Grounded", _grounded);
            _animator.SetInteger("ForwardSpeed", (int)_vInput);

            //Вызов метода MovePosition на нашем компоненте _rb, который принимает параметр Vector3 и соответствующим образом прикладывает силу:
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

        private void Attack()
        {
            Instantiate(_rockperf, _spaunPoint.position, _spaunPoint.rotation); //создаем камень в точке спуна с его позициями
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
                _rocks++;
                Debug.Log("Камень подобран");
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
