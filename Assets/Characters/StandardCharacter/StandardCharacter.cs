using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pratfall.Input;

namespace Pratfall.Characters
{
    public class StandardCharacter : BaseCharacter
    {
        [Header("Physics")]
        public GroundDetector detectGround;
        //PHYSICS PROPERTIES
        public float jumpForce;
        public float runSpeed;
        public float airSpeed;

        [Header("Input")]
        //MOVES
        public Moveset moveset;
        public float delayBetweenInputBuffer;
        float _timeBeforeBufferUpdate;

        InputBuffer inputBuffer;
        InputState inputState;

        void Awake()
        {
            //TODO make this not a magic number
            inputBuffer = new InputBuffer(60);
            _timeBeforeBufferUpdate = delayBetweenInputBuffer;
        }

        Vector2 finalVelocity;
        //INPUT ACTIONS
        public override void OnMove(Vector2 direction)
        {
            inputState.moveValue = direction;

            if (direction.x > 0f && !facingRight)
                Turn();
            else if (direction.x < 0f && facingRight)
                Turn();

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
        //Movement related functions




        public override void OnAltMove(Vector2 direction) { }

        public override void OnJump()
        {
            worldCollider.AddForce(Vector2.up * jumpForce);
        }

        public override void OnBlock() { }


        void MoveFinished(AttackAction move)
        {
            move.Completed -= MoveFinished;
        }
        void MoveStarted(AttackAction move)
        {
            move.Started -= MoveStarted;
        }

        public override void OnAttack()
        {
            InputDirection[] bufferContents = inputBuffer.MovePattern();
            if (!facingRight)
                bufferContents = InputBuffer.InvertHorizontal(bufferContents);

            AttackMove attackMove = moveset.ParseInput(bufferContents);

            if(attackMove != null)
            {
                attackMove.moveToPerform.Completed += MoveFinished;
                attackMove.moveToPerform.Started += MoveStarted;
                attackMove.PerformMove();
            }
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