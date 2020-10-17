using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall.Characters
{
    public class StandardCharacter : BaseCharacter
    {
        public PhysicsModifier physics;
        public HitController hitControl;
        private GroundDetector detectGround;
        //PHYSICS PROPERTIES
        public float jumpForce;
        public float runSpeed;
        public float airSpeed;
        [Space(10f)]
        //MOVES
        public AttackAction ATTACK_JAB;
        public AttackAction SPECIAL_NEUTRAL;

        void Awake()
        {
            detectGround = physics.detectGround;
        }

        Vector2 finalVelocity;
        //INPUT ACTIONS
        public override void OnMove(Vector2 direction)
        {
            //Can only move manually when not in hitstun
            if (hitControl.hitStunTimer <= 0f)
            {
                if (detectGround.groundIsBelow)
                    finalVelocity = new Vector2(detectGround.groundSlope.x, detectGround.groundSlope.y).normalized * runSpeed * direction.x;
                else
                    finalVelocity = Vector2.right * airSpeed * direction.x;
            }
            else
            {
                finalVelocity = Vector3.zero;
            }
        }

        public override void OnAltMove(Vector2 direction) { }

        public override void OnJump()
        {
            worldCollider.AddForce(Vector2.up * jumpForce);
        }

        public override void OnBlock() { }

        public override void OnAttack()
        {
            if (!ATTACK_JAB.midAction)
                ATTACK_JAB.Begin();
        }
        
        public override void OnSpecial()
        {
            if (!SPECIAL_NEUTRAL.midAction)
                SPECIAL_NEUTRAL.Begin();
        }
        //END INPUT ACTIONS

        new void FixedUpdate()
        {
            worldCollider.AddForce(finalVelocity);
        }
    }
}