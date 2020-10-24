using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{
    [RequireComponent(typeof(Collider))]
    public class GroundDetector : MonoBehaviour
    {
        public Rigidbody body;
        private Collider coll;

        private bool m_groundedPreviousFrame;
        public LayerMask detectionMask;
        Vector3 sphereCastorigin;
        float radius;
        const float sphereCastMaxDistance = 0.05f;

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
            coll = GetComponent<Collider>();
        }

        void FixedUpdate()
        {
            sphereCastorigin =  new Vector3(0f, -coll.bounds.extents.y + 0.5f, 0f);
            radius = coll.bounds.extents.x - 0.01f;

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