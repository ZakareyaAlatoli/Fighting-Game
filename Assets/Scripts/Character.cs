using Pratfall.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{
    /// <summary>
    /// Fighters should inherit from this class to handle character-specific mechanics
    /// </summary>
    public abstract class Character : MonoBehaviour, IFighterInput
    {
        public static event System.Action<GameObject> Spawned;
        // Start is called before the first frame update
        protected virtual void Start()
        {
            Spawned?.Invoke(gameObject);
        }

        // Update is called once per frame
        protected virtual void Update()
        {

        }

        protected virtual void FixedUpdate()
        {
            //
        }

        public virtual void OnMove(Vector2 direction)
        {
            throw new System.NotImplementedException();
        }

        public virtual void OnAttack(Vector2 direction)
        {
            throw new System.NotImplementedException();
        }

        public virtual void OnJump()
        {
            throw new System.NotImplementedException();
        }

        public virtual void OnBlock()
        {
            throw new System.NotImplementedException();
        }
    }
}

