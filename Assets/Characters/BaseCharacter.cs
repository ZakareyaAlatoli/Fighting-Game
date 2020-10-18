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
                hurtboxModel.hitTags.team = value;
                _team = value;
            }
        }
        public Sprite sprite;
        public string charName;
        public Rigidbody worldCollider;
        public PhysicsModifier physics;
        public HurtboxModel hurtboxModel;
        public HitController hitControl;
        // Start is called before the first frame update
        protected virtual void Start() { }

        // Update is called once per frame
        protected virtual void Update() { }

        protected virtual void FixedUpdate() { }


        public abstract void OnMove(Vector2 direction);

        public abstract void OnAltMove(Vector2 direction);

        public abstract void OnJump();

        public abstract void OnAttack();

        public abstract void OnBlock();

        public abstract void OnSpecial();
    }
}

