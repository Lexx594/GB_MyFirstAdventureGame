using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Adventure
{
    public class BulletBehaviourScript : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Destroy(gameObject, 5f);
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnCollisionEnter(Collision collision)
        {
            DestroyBullet();
        }
        private void OnTriggerEnter(Collider other)
        {
            DestroyBullet();
        }



        private void DestroyBullet()
        {
            Destroy(gameObject);
        }
    }
}
