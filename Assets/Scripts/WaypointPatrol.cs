using UnityEngine;
using UnityEngine.AI;


namespace Adventure
{

    public class WaypointPatrol : MonoBehaviour
    {

        [SerializeField] private PlayerBehaviourScript _playerScript; //ссылка на основной скрипт игрока
        public Transform player; //ссылка на игрока (преобразование игрока)
        public LayerMask whatIsGround, whatIsPlayer; //слои земли и игрока

        public float sightRange, attackRange; // переменные для дальности видимости и дальности атаки
        public bool playerInSightRange, playerInAttackRange; //для проверки нахождения игрока в зоне досигаемости
        public bool playerInLineSight;


        [SerializeField] private AudioSource _audsDestroy;
        [SerializeField] private AudioSource _audsPlayerKill;

        public NavMeshAgent navMeshAgent;
        public Transform[] waypoints;

        int m_CurrentWaypointIndex;


        private void Awake()
        {
            player = GameObject.Find("Player").transform; //находим игрока по имени
            navMeshAgent.SetDestination(waypoints[0].position);
        }


        void Update()
        {
            
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
            if (!playerInSightRange && !playerInAttackRange) Patroling();
            //если игрок в зоне видимости, но не в зоне атаки враг должен преследовать игрока
            if (playerInSightRange && !playerInAttackRange && flag) ChasePlayer();
            // если игрок в зоне видимости и в зоне атаки враг должен атаковать игрока
            if (playerInSightRange && playerInAttackRange && flag) AttackPlayer();

        }

        private void Patroling()
        {
            if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
            {
                m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            }
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false); //деактивируем 1 ребенка (вспышка)
        }

        private void ChasePlayer()
        {
            //говорим что агент назначил позицию игрока
            navMeshAgent.SetDestination(player.position);
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true); //активируем 1 ребенка (вспышка)
        }
        bool flagAttack = true;
        private void AttackPlayer()
        {
            
            if (flagAttack)
            {
                Debug.Log("Бум");
                _audsPlayerKill.Play();
                // здесь необходимо прописать код атаки
                _playerScript.Death();
                flagAttack = false;
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if(other.tag == "enemy")
            {
                _audsDestroy.Play();
                Destroy(other.gameObject, 2f);
            }

            if (other.name == "ToxicRockСast Variant(Clone)")
            {
                gameObject.GetComponent<NavMeshAgent>().speed = gameObject.GetComponent<NavMeshAgent>().speed / 1.3f;
            }


        }    

        private void OnDrawGizmosSelected() //визуализируем атаку и начало преследования
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sightRange);

        }

    }
}
