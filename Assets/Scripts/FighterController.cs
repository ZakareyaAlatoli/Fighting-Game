using System;
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

        private bool moveEnabled;
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
            if(detectGround.grounded)
                body.AddForce(Vector3.up * jumpForce);
        }

        // Update is called once per frame
        void Update()
        {
            //HANDLE ALL INPUTS
            //MOVE
            MoveHorizontal(move.ReadValue<float>());
            //JUMP
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Jump();
            }
            //ATTACK
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Attack();
            }
            //SPECIAL
            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                Special();
            }
            //BLOCK
            if (Mouse.current.middleButton.wasPressedThisFrame)
            {
                StartBlock();
            }
            else if (Mouse.current.middleButton.wasReleasedThisFrame)
            {
                ReleaseBlock();
            }


        }

        private void ReleaseBlock()
        {
            throw new NotImplementedException();
        }

        private void StartBlock()
        {
            throw new NotImplementedException();
        }

        private void Special()
        {
            throw new NotImplementedException();
        }

        private void Attack()
        {
            throw new NotImplementedException();
        }
    }
}