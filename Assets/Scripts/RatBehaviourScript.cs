using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Adventure
{


    public class RatBehaviourScript : MonoBehaviour
    {
        public GameObject Rat;
        [SerializeField] private List<Transform> _ratSpown;

        //bool _active = false;




        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter(Collider other)
        {

            if(other.name == "Player")
            {
                foreach(Transform point in _ratSpown)
                {
                    Instantiate(Rat, point.position, Quaternion.identity);
                }
                Destroy(this.gameObject);

            }
        }





    }
}
