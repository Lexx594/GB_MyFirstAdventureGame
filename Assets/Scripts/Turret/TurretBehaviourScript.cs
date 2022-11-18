using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Adventure
{
    public class TurretBehaviourScript : MonoBehaviour
    {


        [Header("����� ���������")]
        [SerializeField] private GameObject _bulletPrefab; //������ �� ������ ����
        [SerializeField] private Transform _spawnPoint; //������ �� ����� ��������� ���� 
        [SerializeField] private Transform _player; //������ �� ������ 
        [SerializeField] private LayerMask _whatIsPlayer; //������ �� ���� ������
        [SerializeField] private AudioSource _shoot; //������ �� ����
        [SerializeField] private float _rotationSpeed; //�������� �������� ������


        [Header("��������� �����")]
        [SerializeField] private float _timeBetweenAttacks; //����� ����� �������
        [SerializeField] private float _sightRange, _attackRange; // ���������� ��� ��������� ��������� � ��������� �����
        
        bool _alreadyAttaked; //���� �� ��� �����                
        bool _playerInSightRange, _playerInAttackRange; //��� �������� ���������� ������ � ���� ������������



        private void Awake()
        {
            _player = GameObject.Find("Player").transform; //������� ������ �� �����
            
        }

        private void Update()
        {
            // ��������� ��������� �� ����� �� ��������� �����
            _playerInSightRange = Physics.CheckSphere(transform.position, _sightRange, _whatIsPlayer); //���� ��������� ������ ����� ����������� ����� ������������ ���� �������, ��������� ��������� � ����� ���� ������
            _playerInAttackRange = Physics.CheckSphere(transform.position, _attackRange, _whatIsPlayer); //���������� �� ��� �����


            //���� ����� � ���� ���������, �� �� � ���� ����� ���� ������ ������������ ������
            if (_playerInSightRange && !_playerInAttackRange) LookATPlayer(); 
            // ���� ����� � ���� ��������� � � ���� ����� ���� ������ ��������� ������
            if (_playerInSightRange && _playerInAttackRange) AttackPlayer();
        }

        private void LookATPlayer() //������� ������� �� ������
        {
            var direction = _player.transform.position - transform.position;
            var rotation = Vector3.RotateTowards(transform.forward, direction, _rotationSpeed * Time.deltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(rotation);


        }



        private void AttackPlayer()
        {

            //��������� �� ������
            LookATPlayer();

            // ���� �� ��� ���������, �� �������� ����� ������ �����
            if (!_alreadyAttaked)
            {
                // ����� ���������� ��������� ��� �����
                Rigidbody rb = Instantiate(_bulletPrefab, _spawnPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
                rb.AddForce(transform.up * 1f, ForceMode.Impulse);
                _shoot.Play();

                _alreadyAttaked = true;
                Invoke(nameof(ResetAttack), _timeBetweenAttacks);
            }

        }

        private void ResetAttack() //����� �����
        {
            _alreadyAttaked = false;
        }

        private void OnDrawGizmosSelected() //������������� ����� � ������ �������������
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _sightRange);

        }

    }
}
