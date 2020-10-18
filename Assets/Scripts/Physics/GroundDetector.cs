using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{
    public class GroundDetector : MonoBehaviour
    {
        public Rigidbody body;
        private bool m_groundedPreviousFrame;
        public LayerMask detectionMask;
        public Vector3 sphereCastorigin;
        public float radius;
        public float sphereCastMaxDistance;

        private bool _grounded;
        public bool grounded { get => _grounded; }
        private bool m_groundIsBelow;
        public bool groundIsBelow { get => m_groundIsBelow; }
        public Vector3 groundSlope { get; private set; }

        public event System.Action Landed;
        public event System.Action BecomeAirborne;

        // Start is called before the first frame update
        void Start()
        {
            if (body == null)
                body = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            RaycastHit hitInfo;
            m_groundIsBelow = Physics.SphereCast(transform.position + sphereCastorigin, radius, Vector3.down, out hitInfo, sphereCastMaxDistance, detectionMask);
            if (m_groundIsBelow)
            {
                groundSlope = Vector3.Cross(hitInfo.normal, Vector3.forward).normalized;
                if (!m_groundedPreviousFrame)
                {
                    _grounded = true;
                    Landed?.Invoke();
                }
            }
            else
            {
                groundSlope = Vector3.zero;
                if (m_groundedPreviousFrame)
                {
                    _grounded = false;
                    BecomeAirborne?.Invoke();
                }
            }        

            m_groundedPreviousFrame = _grounded;
        }
    }
}