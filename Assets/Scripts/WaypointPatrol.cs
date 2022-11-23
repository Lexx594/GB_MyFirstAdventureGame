using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{


    public Transform player; //ссылка на игрока (преобразование игрока)
    public LayerMask whatIsGround, whatIsPlayer; //слои земли и игрока

    public float sightRange, attackRange; // переменные для дальности видимости и дальности атаки
    public bool playerInSightRange, playerInAttackRange; //для проверки нахождения игрока в зоне досигаемости



    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;

    int m_CurrentWaypointIndex;


    private void Awake()
    {
        player = GameObject.Find("Player").transform; //находим игрока по имени
        navMeshAgent.SetDestination(waypoints[0].position);
    }


    void Update ()
    {

        // проверяем находится ли игрок на растоянии атаки
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer); //зона видимости игрока равна контрольной сфере используемой свою позицию, диапазона видимости и маски слоя игрока
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer); //аналогично но для атаки

        //если игрок не в зоне видимости и не в зоне атаки враг должен патрулировать
        if (!playerInSightRange && !playerInAttackRange) Patroling();
        //если игрок в зоне видимости, но не в зоне атаки враг должен преследовать игрока
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        // если игрок в зоне видимости и в зоне атаки враг должен атаковать игрока
        if (playerInSightRange && playerInAttackRange) AttackPlayer();

    }

    private void Patroling()
    {
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        }
    }

    private void ChasePlayer()
    {
        //говорим что агент назначил позицию игрока
        navMeshAgent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {

        // здесь необходимо прописать код атаки
        Destroy(player);

    }

    private void OnDrawGizmosSelected() //визуализируем атаку и начало преследования
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

    }


}
