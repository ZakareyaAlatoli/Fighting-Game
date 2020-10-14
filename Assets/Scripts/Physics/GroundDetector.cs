﻿using System.Collections;
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

        public Vector3 raycastOrigin;
        public float raycastMaxDistance;
        private bool _grounded;
        public bool grounded { get => _grounded; }
        private bool m_groundIsBelow;
        public Vector3 groundSlope { get; private set; }

        public event System.Action Landed;
        public event System.Action BecomeAirborne;

        // Start is called before the first frame update
        void Start()
        {
            if (body == null)
                body = GetComponent<Rigidbody>();
        }

        void Update()
        {

            RaycastHit hitInfo;
            m_groundIsBelow = Physics.SphereCast(transform.position + sphereCastorigin, radius, Vector3.down, out hitInfo, sphereCastMaxDistance, detectionMask);
            if (m_groundIsBelow)
            {
                if (Physics.Raycast(transform.position + raycastOrigin, Vector3.down, out hitInfo, raycastMaxDistance, detectionMask))
                {
                    groundSlope = Vector3.Cross(hitInfo.normal, Vector3.forward).normalized;
                }
                else
                {
                    groundSlope = Vector3.zero;
                }
            }           
            else
                groundSlope = Vector3.zero;
            //If we were airborne last frame and grounded this frame
            if (!m_groundedPreviousFrame && m_groundIsBelow)
            {
                    _grounded = true;
                    Landed?.Invoke();    
            }
            //If we were grounded last frame and airborne this frame
            else if(m_groundedPreviousFrame && !m_groundIsBelow)
            {
                _grounded = false;
                BecomeAirborne?.Invoke();
            }

            m_groundedPreviousFrame = _grounded;
        }
    }
}