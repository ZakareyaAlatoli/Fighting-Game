using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{
    public class GroundDetector : MonoBehaviour
    {
        public LayerMask detectionMask;
        public Vector3 origin;
        public float maxDistance;
        private bool _grounded;
        public bool grounded { get => _grounded; }

        // Start is called before the first frame update
        void Start() {}

        // Update is called once per frame
        void Update()
        {
            _grounded = Physics.Raycast(transform.position + origin, Vector3.down, maxDistance, detectionMask);
        }
    }
}