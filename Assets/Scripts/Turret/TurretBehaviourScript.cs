using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Adventure
{
    public class TurretBehaviourScript : MonoBehaviour
    {


        [Header("Общие настройки")]
        [SerializeField] private GameObject _bulletPrefab; //ссылка на префаб пули
        [SerializeField] private Transform _spawnPoint; //ссылка на точку появления пули 
        [SerializeField] private Transform _player; //ссылка на игрока 
        [SerializeField] private LayerMask _whatIsPlayer; //ссылка на слой игрока
        [SerializeField] private AudioSource _shoot; //ссылка на зкук
        [SerializeField] private float _rotationSpeed; //скорость поворота турели


        [Header("Настройки атаки")]
        [SerializeField] private float _timeBetweenAttacks; //время между атаками
        [SerializeField] private float _sightRange, _attackRange; // переменные для дальности видимости и дальности атаки
        
        bool _alreadyAttaked; //идет ли уже атака                
        bool _playerInSightRange, _playerInAttackRange; //для проверки нахождения игрока в зоне досигаемости



        private void Awake()
        {
            _player = GameObject.Find("Player").transform; //находим игрока по имени
            
        }

        private void Update()
        {
            // проверяем находится ли игрок на растоянии атаки
            _playerInSightRange = Physics.CheckSphere(transform.position, _sightRange, _whatIsPlayer); //зона видимости игрока равна контрольной сфере используемой свою позицию, диапазона видимости и маски слоя игрока
            _playerInAttackRange = Physics.CheckSphere(transform.position, _attackRange, _whatIsPlayer); //аналогично но для атаки


            //если игрок в зоне видимости, но не в зоне атаки враг должен преследовать игрока
            if (_playerInSightRange && !_playerInAttackRange) LookATPlayer(); 
            // если игрок в зоне видимости и в зоне атаки враг должен атаковать игрока
            if (_playerInSightRange && _playerInAttackRange) AttackPlayer();
        }

        private void LookATPlayer() //плавный поворот на игрока
        {
            var direction = _player.transform.position - transform.position;
            var rotation = Vector3.RotateTowards(transform.forward, direction, _rotationSpeed * Time.deltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(rotation);


        }



        private void AttackPlayer()
        {

            //наводимся на игрока
            LookATPlayer();

            // если мы уже атаковали, то вызываем метод сброса атаки
            if (!_alreadyAttaked)
            {
                // здесь необходимо прописать код атаки
                Rigidbody rb = Instantiate(_bulletPrefab, _spawnPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
                rb.AddForce(transform.up * 1f, ForceMode.Impulse);
                _shoot.Play();

                _alreadyAttaked = true;
                Invoke(nameof(ResetAttack), _timeBetweenAttacks);
            }

        }

        private void ResetAttack() //сброс атаки
        {
            _alreadyAttaked = false;
        }

        private void OnDrawGizmosSelected() //визуализируем атаку и начало преследования
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _sightRange);

        }

    }
}
