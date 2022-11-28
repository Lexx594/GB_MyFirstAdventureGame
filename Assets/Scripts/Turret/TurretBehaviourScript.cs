using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

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

            //var direction = player.transform.position - transform.position;
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);
            Debug.DrawRay(transform.position, transform.forward, Color.yellow);
            bool flag = false;

            if (Physics.Raycast(ray, out hit, _sightRange) && hit.collider != null)
            {
                //Debug.Log(hit.collider.name);

                if (hit.collider.tag == _player.tag) flag = true;
                else flag = false;
            }
            else flag = false;


            //if(hit.collider == null)
            //{

            //}

            // ��������� ��������� �� ����� �� ��������� �����
            _playerInSightRange = Physics.CheckSphere(transform.position, _sightRange, _whatIsPlayer); //���� ��������� ������ ����� ����������� ����� ������������ ���� �������, ��������� ��������� � ����� ���� ������
            _playerInAttackRange = Physics.CheckSphere(transform.position, _attackRange, _whatIsPlayer); //���������� �� ��� �����


            //���� ����� � ���� ���������, �� �� � ���� ����� ���� ������ ������������ ������
            if (_playerInSightRange && !_playerInAttackRange) LookATPlayer(); 
            // ���� ����� � ���� ��������� � � ���� ����� ���� ������ ��������� ������
            if (_playerInSightRange && _playerInAttackRange && flag ) AttackPlayer();


            

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
