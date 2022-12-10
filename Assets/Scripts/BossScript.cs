using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Adventure
{
    public class BossScript : MonoBehaviour
    {
        [SerializeField] private PlayerBehaviourScript _playerScript; //������ �� �������� ������ ������

        [SerializeField] private Transform player;

        [SerializeField] private NavMeshAgent navMeshAgent;

        [SerializeField] private AudioSource _audsAttack;
        [SerializeField] private AudioSource _audsDeath;

        [SerializeField] private LayerMask whatIsPlayer; //���� ����� � ������
        [SerializeField] private float sightRange, attackRange; // ���������� ��� ��������� ��������� � ��������� �����
        
        [SerializeField] private Transform _waypoints;


        public bool playerInSightRange, playerInAttackRange; //��� �������� ���������� ������ � ���� ������������
        public bool playerInLineSight;

        public bool playerInZone;

        private Animator _animator;
        private bool _isWalk;
        private bool _isDeath;





        private void Awake()
        {
            player = GameObject.Find("Player").transform; //������� ������ �� �����
            _animator = GetComponent<Animator>();
            navMeshAgent.SetDestination(_waypoints.position);
        }



        void Update()
        {
            _animator.SetBool("Walk", _isWalk);
            

            var direction = player.transform.position - transform.position;
            RaycastHit hit;
            Ray ray = new Ray(transform.position, direction);
            bool flag = false;

            // ��������� ��������� �� ����� �� ��������� �����
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer); //���� ��������� ������ ����� ����������� ����� ������������ ���� �������, ��������� ��������� � ����� ���� ������
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer); //���������� �� ��� �����

            playerInLineSight = Physics.Raycast(ray, out hit, sightRange);
            //playerInLineSight = Physics.Linecast(transform.position, player.position, whatIsPlayer);


            if (hit.collider != null)
            {
                //Debug.Log(hit.collider.name);

                if (hit.collider.tag == player.tag) flag = true;
                else flag = false;
            }
            else flag = false;




            Debug.DrawRay(transform.position, direction, Color.red);
            //Debug.DrawLine(transform.position, player.position, Color.blue);
            //���� ����� �� � ���� ��������� � �� � ���� ����� ���� ������ �������������
            if (!playerInSightRange && !playerInAttackRange && !_isDeath) Patroling();
            //���� ����� � ���� ���������, �� �� � ���� ����� ���� ������ ������������ ������
            if (playerInSightRange && !playerInAttackRange && !_isDeath && playerInZone && flag) ChasePlayer();
            // ���� ����� � ���� ��������� � � ���� ����� ���� ������ ��������� ������
            if (playerInSightRange && playerInAttackRange && !_isDeath && playerInZone && flag) AttackPlayer();

        }


        private void Patroling()
        {
            if (navMeshAgent.remainingDistance >= 0.1)
            {
                navMeshAgent.SetDestination(_waypoints.position);
                _isWalk = true;
            }
            else _isWalk= false;
        }

        private void ChasePlayer()
        {
            //������� ��� ����� �������� ������� ������
            navMeshAgent.SetDestination(player.position);            
        }
        bool flagAttack = true;
        private void AttackPlayer()
        {

            if (flagAttack)
            {
                _animator.SetTrigger("Attack");
                _audsAttack.Play();
                _playerScript.Death();
                flagAttack = false;
            }
        }

        public void Death()
        {
            _animator.SetTrigger("Death");
            _audsDeath.Play();
            _isDeath = true;
        }



    }
}
