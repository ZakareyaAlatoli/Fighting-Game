using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pratfall
{
    public class FighterController : MonoBehaviour
    {
        public InputAction move;
        public Rigidbody body;
        public GroundDetector detectGround;
        public float groundSpeed = 40f;
        public float airSpeed = 20f;
        public float jumpForce = 50f;
        // Start is called before the first frame update
        void Start()
        {
            move.Enable();
        }

        public void MoveHorizontal(float input)
        {
            if(detectGround.grounded)
                body.AddForce(new Vector3(input * groundSpeed, 0f, 0f));
            else
                body.AddForce(new Vector3(input * airSpeed, 0f, 0f));
        }

        public void Jump()
        {
            body.AddForce(Vector3.up * jumpForce);
        }

        // Update is called once per frame
        void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                body.AddForce(Vector3.up * jumpForce);
            }
            MoveHorizontal(move.ReadValue<float>());

        }
    }
}