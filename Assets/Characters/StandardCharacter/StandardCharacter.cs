using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pratfall.Input;

namespace Pratfall.Characters
{
    public class StandardCharacter : BaseCharacter
    {
        public GroundDetector detectGround;
        //PHYSICS PROPERTIES
        public float jumpForce;
        public float runSpeed;
        public float airSpeed;
        [Header("Moveset")]
        //MOVES
        public float delayBetweenInputBuffer;
        float _timeBeforeBufferUpdate;
        public AttackMove ATTACK_JAB;

        InputBuffer inputBuffer;
        InputState inputState;

        void Awake()
        {
            inputBuffer = new InputBuffer(60);
            _timeBeforeBufferUpdate = delayBetweenInputBuffer;
        }

        Vector2 finalVelocity;
        //INPUT ACTIONS
        public override void OnMove(Vector2 direction)
        {
            inputState.moveValue = direction;
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
            if (ATTACK_JAB.MatchInput(inputBuffer.MovePattern()))
                ATTACK_JAB.PerformMove();
        }
        
        public override void OnSpecial()
        {

        }
        //END INPUT ACTIONS
        new void Update()
        {
            if (_timeBeforeBufferUpdate < 0f)
            {
                _timeBeforeBufferUpdate = delayBetweenInputBuffer;
                inputBuffer.Next(inputState);
            }
            _timeBeforeBufferUpdate -= Time.deltaTime;
        }


        new void FixedUpdate()
        {
            worldCollider.AddForce(finalVelocity);
        }
    }
}