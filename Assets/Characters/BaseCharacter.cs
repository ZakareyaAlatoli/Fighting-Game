using Pratfall.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pratfall.Characters
{
    /// <summary>
    /// Fighters should inherit from this class to handle character-specific mechanics
    /// </summary>
    public abstract class BaseCharacter : MonoBehaviour, IControllable
    {
        protected int _team;
        public int team {
            get => _team;
            set
            {
                hitControl.hitTags.team = value;
                _team = value;
            }
        }
        public Sprite sprite;
        public string charName;
        public Rigidbody worldCollider;
        public PhysicsModifier physics;
        public HitController hitControl;

        void Awake()
        {
            hitControl.hitTags.origin = gameObject;
            hitControl.hitTags.team = team;
            facingRight = true;
        }

        // Start is called before the first frame update
        protected virtual void Start() { }

        // Update is called once per frame
        protected virtual void Update() { }

        protected virtual void FixedUpdate() { }


        public abstract void OnMove(Vector2 direction);

        public abstract void OnAltMove(Vector2 direction);

        public abstract void OnJump();
        public abstract void OnJumpReleased();

        public abstract void OnAttack();
        public abstract void OnAttackReleased();

        public abstract void OnBlock();
        public abstract void OnBlockReleased();

        public abstract void OnSpecial();
        public abstract void OnSpecialReleased();







        //FUNCTIONS COMMON TO ALL CHARACTERS

        #region Turning
        public bool facingRight { get; private set; }
        public float turnSpeed;
        Coroutine turnOverTime;
        bool midTurn;
        IEnumerator TurnOverTime(Transform objectToRotate, Quaternion direction, float turnSpeed)
        {
            midTurn = true;
            while (objectToRotate.rotation != direction)
            {
                objectToRotate.rotation = Quaternion.SlerpUnclamped(objectToRotate.rotation, direction, turnSpeed * Time.deltaTime);
                yield return null;
            }
            midTurn = false;
        }
        public void Turn()
        {
            facingRight = !facingRight;
            if (midTurn)
                StopCoroutine(turnOverTime);
            if (facingRight)
            {
                turnOverTime = StartCoroutine(TurnOverTime(worldCollider.transform,
                                                           Quaternion.LookRotation(Vector3.forward, Vector3.up),
                                                           turnSpeed));
            }
            else
            {
                turnOverTime = StartCoroutine(TurnOverTime(worldCollider.transform,
                                       Quaternion.LookRotation(Vector3.back, Vector3.up),
                                       turnSpeed));
            }
        }
        #endregion
    }
}

