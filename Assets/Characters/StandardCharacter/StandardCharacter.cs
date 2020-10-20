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

        [Space(10f)]
        public Shield shield;

        [Header("Input")]
        //MOVES
        public Moveset groundedMoveset;
        public Moveset aerialMoveset;
        public float delayBetweenInputBuffer;
        float _timeBeforeBufferUpdate;

        InputBuffer inputBuffer;
        InputState inputState;

        protected override void Start()
        {
            //TODO make this not a magic number
            inputBuffer = new InputBuffer(60);
            _timeBeforeBufferUpdate = delayBetweenInputBuffer;
            //Attach shield to character
            shield.shieldBubble.hitTags.origin = gameObject;
            shield.shieldBubble.hitTags.team = team;
            FollowObject(shield.transform, worldCollider.transform);
            //Handle behavior when put into hitstun
            
        }
        bool shielding;
        void StartedShielding() { shielding = true; }
        void StoppedShielding() { shielding = false; }
        void OnEnable()
        {
            hitControl.EnteredHitstun += OnHitstunned;
            shield.ShieldOn += StartedShielding;
            shield.ShieldOff += StoppedShielding;
        }
        void OnDisable()
        {
            hitControl.EnteredHitstun -= OnHitstunned;
            shield.ShieldOn -= StartedShielding;
            shield.ShieldOff -= StoppedShielding;
        }

        void OnHitstunned()
        {
            shield.TurnOff();
            currentlyPerformingMove?.moveToPerform.Stop();
        }

        Vector2 finalVelocity;
        //INPUT ACTIONS
        public override void OnMove(Vector2 direction)
        {
            if (Mathf.Abs(direction.x) < InputBuffer.stickDeadzone)
                direction.x = 0f;
            inputState.moveValue = direction;
            if (detectGround.grounded && !moveLockout && !shielding && hitControl.hitStun <= 0f)
            {
                if (direction.x > 0 && !facingRight)
                    Turn();
                else if (direction.x < 0 && facingRight)
                    Turn();
            }

            //Can only move manually when not in hitstun
            if (hitControl.hitStun > 0f)
            {
                finalVelocity = Vector3.zero;
                return;
            }
            if (shielding)
            {
                finalVelocity = Vector3.zero;
                return;
            }
            if (moveLockout)
            {
                finalVelocity = Vector3.zero;
                if (!detectGround.groundIsBelow)
                    finalVelocity = Vector2.right * airSpeed * direction.x;
                else
                    finalVelocity = Vector3.zero;
                return;
            }
            if (detectGround.groundIsBelow)
                finalVelocity = new Vector2(detectGround.groundSlope.x, detectGround.groundSlope.y).normalized * runSpeed * direction.x;
            else
                finalVelocity = Vector2.right * airSpeed * direction.x;
        }
        //Movement related functions




        public override void OnAltMove(Vector2 direction) { }

        public override void OnJump()
        {
            if(!moveLockout && !shielding && detectGround.grounded)
                worldCollider.AddForce(Vector2.up * jumpForce);
        }

        public override void OnJumpReleased() { }

        public override void OnBlock()
        {
            if(detectGround.groundIsBelow)
                shield.TurnOn();
        }

        public override void OnBlockReleased()
        {
            shield.TurnOff();
        }





        //When the player presses the Attack button, the game will pass the input
        //buffer to the Movelist. If the input buffer has a string of directional
        //inputs corresponding to a move, that move will be performed
        bool moveLockout;
        AttackMove currentlyPerformingMove;
        public override void OnAttack()
        {
            if (shielding || hitControl.hitStun > 0)
                return;

            InputDirection[] bufferContents = inputBuffer.MovePattern();
            if (!facingRight)
                bufferContents = InputBuffer.InvertHorizontal(bufferContents);

            AttackMove attackMove;
            if (detectGround.grounded)
                attackMove = groundedMoveset.ParseInput(bufferContents);
            else
                attackMove = aerialMoveset.ParseInput(bufferContents);

            currentlyPerformingMove = attackMove;
            if (attackMove != null)
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
        public override void OnAttackReleased() { }

        void OnMoveBegin(DynamicAction move)
        {
            move.FullyCompleted += OnEndMove;
            move.Interrupted += OnMoveInterrupted;
        }
        void OnEndMove(DynamicAction move)
        {
            move.Started -= OnMoveBegin;
            move.FullyCompleted -= OnEndMove;
            move.enabled = true;
            moveLockout = false;
        }
        void OnMoveInterrupted(DynamicAction move)
        {
            OnEndMove(move);
            move.Interrupted -= OnMoveInterrupted;
        }

        public override void OnSpecial() { }
        public override void OnSpecialReleased() { }

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



        Coroutine FollowObject(Transform follower, Transform followee)
        {
            return StartCoroutine(CR_FollowObject(follower, followee));
        }

        IEnumerator CR_FollowObject(Transform follower, Transform followee)
        {
            while (follower != null && followee != null)
            {
                follower.position = followee.position;
                yield return null;
            }
        }
    }
}