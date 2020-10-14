﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall.Characters
{
    public class StandardCharacter : Character
    {
        public CompoundCollider hurtbox;
        public PhysicsModifier physics;
        private GroundDetector detectGround;
        public float jumpForce;
        public float runSpeed;
        public float airSpeed;

        Vector2 finalVelocity;
        public override void OnMove(Vector2 direction)
        {
            if (detectGround.grounded)
            {
                finalVelocity = new Vector2(detectGround.groundSlope.x, detectGround.groundSlope.y).normalized * runSpeed * direction.x;
            }
            else
            {
                finalVelocity = Vector2.right * airSpeed * direction.x;
            }
        }

        public override void OnJump()
        {
            worldCollider.AddForce(Vector2.up * jumpForce);
        }

        new void FixedUpdate()
        {
            worldCollider.AddForce(finalVelocity);
        }

        new void Start()
        {
            base.Start();
            
        }

        void Awake()
        {
            detectGround = physics.detectGround;
        }

        public override void OnAttack(Vector2 direction)
        {

        }

        public override void OnBlock()
        {

        }
    }
}