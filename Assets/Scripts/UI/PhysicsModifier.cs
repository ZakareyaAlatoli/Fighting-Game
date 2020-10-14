using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{ 
    [RequireComponent(typeof(Rigidbody))]
    public class PhysicsModifier : MonoBehaviour
    {
        private Rigidbody body;
        public float gravityMultiplier = 1f;
        public float terminalVelocity = 9.8f;
        public float traction = 1f;
        public float airResistance = 1f;
        public GroundDetector detectGround;
        private static float gravityCorrection = 240f;
        // Start is called before the first frame update
        void Awake()
        {
            body = GetComponent<Rigidbody>();
            detectGround.Landed += DetectGround_Landed;
        }

        void DetectGround_Landed()
        {
            Debug.Log("LANDED");
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Vector3 finalVelocity = body.velocity;
            ///DO MODIFICATIONS HERE
            if (!detectGround.grounded)
            {
                finalVelocity.y = Mathf.Lerp(finalVelocity.y, -terminalVelocity, -Physics.gravity.y * (gravityMultiplier / gravityCorrection) * Time.fixedDeltaTime);
                finalVelocity.x = Mathf.Lerp(finalVelocity.x, 0f, airResistance * Time.fixedDeltaTime);
            }
            else
            {
                finalVelocity = Vector3.Lerp(finalVelocity, Vector3.zero, traction * Time.fixedDeltaTime);
                //finalVelocity.x = Mathf.Lerp(finalVelocity.x, 0f, traction * Time.fixedDeltaTime);
            }
            ///---------------------
            body.velocity = finalVelocity;
        }
    }
}