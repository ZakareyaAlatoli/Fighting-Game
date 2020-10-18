using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{ 
    [RequireComponent(typeof(Rigidbody))]
    public abstract class PhysicsModifier : MonoBehaviour
    {
        protected Rigidbody body;
        // Start is called before the first frame update
        void Awake()
        {
            body = GetComponent<Rigidbody>();
        }


        // Update is called once per frame
        protected virtual void FixedUpdate() { }

    }
}

