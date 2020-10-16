using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{
    public class PassthroughCollider : MonoBehaviour
    {
        private Collider coll;
        // Start is called before the first frame update
        void Start()
        {
            coll = GetComponent<Collider>();
            //The first collider on this game object should be the solid part
        }

        void OnTriggerEnter(Collider other)
        {
            Physics.IgnoreCollision(coll, other, true);
        }
        void OnTriggerExit(Collider other)
        {
            Physics.IgnoreCollision(coll, other, false);
        }
    }
}