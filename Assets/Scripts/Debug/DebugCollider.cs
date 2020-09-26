using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall.Debugging
{
    public class DebugCollider : MonoBehaviour
    {
        public CompoundCollider target;
        // Start is called before the first frame update
        void Start()
        {
            target.TriggerEntered += target => Debug.Log(target.name + " triggered " + name);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}