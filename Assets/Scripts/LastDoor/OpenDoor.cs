using UnityEngine;

namespace Adventure
{
    public class OpenDoor : MonoBehaviour
    {
        [SerializeField] private AudioSource _openDoor;
        public bool lewerLeft = false;
        public bool lewerCentr = false;
        public bool lewerRight = true;
        private bool _active = true;
        void Update()
        {
            if (lewerLeft && lewerCentr && lewerRight && _active)
            {
                _openDoor.Play();
                gameObject.GetComponent<Animator>().enabled = true;
                _active = false;
            }

        }
    }
}
