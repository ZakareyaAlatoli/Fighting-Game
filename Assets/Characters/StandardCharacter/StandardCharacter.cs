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
        public Moveset groundedMoveset;
        public Moveset aerialMoveset;
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
            if (detectGround.grounded && !moveLockout)
            {
                if (direction.x > 0f && !facingRight)
                    Turn();
                else if (direction.x < 0f && facingRight)
                    Turn();
            }

            //Can only move manually when not in hitstun
            if (hitControl.hitStunTimer <= 0f)
            {
                if (detectGround.groundIsBelow && !moveLockout)
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
            if(!moveLockout && detectGround.grounded)
                worldCollider.AddForce(Vector2.up * jumpForce);
        }

        public override void OnBlock() { }





        //When the player presses the Attack button, the game will pass the input
        //buffer to the Movelist. If the input buffer has a string of directional
        //inputs corresponding to a move, that move will be performed
        bool moveLockout;
        public override void OnAttack()
        {
            InputDirection[] bufferContents = inputBuffer.MovePattern();
            if (!facingRight)
                bufferContents = InputBuffer.InvertHorizontal(bufferContents);

            AttackMove attackMove;
            if (detectGround.grounded)
                attackMove = groundedMoveset.ParseInput(bufferContents);
            else
                attackMove = aerialMoveset.ParseInput(bufferContents);

            if(attackMove != null)
            {
                if (!moveLockout)
                {
                    attackMove.moveToPerform.Started += OnMoveBegin;
                    attackMove.PerformMove();
                    attackMove.moveToPerform.enabled = false;
                    moveLockout = true;
                }
            }
        }
        void OnMoveBegin(DynamicAction move)
        {
            move.FullyCompleted += OnEndMove;
        }
        void OnEndMove(DynamicAction move)
        {
            move.Started -= OnMoveBegin;
            move.FullyCompleted -= OnEndMove;
            move.enabled = true;
            moveLockout = false;
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