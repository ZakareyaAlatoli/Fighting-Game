using Pratfall.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall.Characters
{
    /// <summary>
    /// Fighters should inherit from this class to handle character-specific mechanics
    /// </summary>
    public abstract class Character : MonoBehaviour, IControllable
    {
        // Start is called before the first frame update
        protected virtual void Start()
        {

        }

        // Update is called once per frame
        protected virtual void Update()
        {

        }

        protected virtual void FixedUpdate()
        {
            //
        }

        public abstract void OnMove(Vector2 direction);

        public abstract void OnAttack(Vector2 direction);

        public abstract void OnJump();

        public abstract void OnBlock();
    }
}

