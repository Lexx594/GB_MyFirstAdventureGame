using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Adventure
{
    public class BossScript : MonoBehaviour
    {
        [SerializeField] private PlayerBehaviourScript _playerScript; //ссылка на основной скрипт игрока

        [SerializeField] private Transform player;

        [SerializeField] private NavMeshAgent navMeshAgent;

        [SerializeField] private AudioSource _audsAttack;
        [SerializeField] private AudioSource _audsDeath;

        [SerializeField] private LayerMask whatIsPlayer; //слои земли и игрока
        [SerializeField] private float sightRange, attackRange; // переменные для дальности видимости и дальности атаки
        
        [SerializeField] private Transform _waypoints;


        public bool playerInSightRange, playerInAttackRange; //для проверки нахождения игрока в зоне досигаемости
        public bool playerInLineSight;

        public bool playerInZone;

        private Animator _animator;
        private bool _isWalk;
        private bool _isDeath;





        private void Awake()
        {
            player = GameObject.Find("Player").transform; //находим игрока по имени
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

            // проверяем находится ли игрок на растоянии атаки
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer); //зона видимости игрока равна контрольной сфере используемой свою позицию, диапазона видимости и маски слоя игрока
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer); //аналогично но для атаки

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
            //если игрок не в зоне видимости и не в зоне атаки враг должен патрулировать
            if (!playerInSightRange && !playerInAttackRange && !_isDeath) Patroling();
            //если игрок в зоне видимости, но не в зоне атаки враг должен преследовать игрока
            if (playerInSightRange && !playerInAttackRange && !_isDeath && playerInZone && flag) ChasePlayer();
            // если игрок в зоне видимости и в зоне атаки враг должен атаковать игрока
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
            //говорим что агент назначил позицию игрока
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
