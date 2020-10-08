using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall.Characters
{
    public class TestCharacter : Character
    {
        public Rigidbody worldCollider;
        public CompoundCollider hurtbox;
        public float jumpForce;
        public float runSpeed;

        public override void OnMove(Vector2 direction)
        {
            worldCollider.AddForce(new Vector2(direction.x * runSpeed, 1f));
        }

        public override void OnJump()
        {
            worldCollider.AddForce(Vector2.up * jumpForce);
        }

        new void Start()
        {
            base.Start();
        }

        public override void OnAttack(Vector2 direction)
        {
            throw new System.NotImplementedException();
        }

        public override void OnBlock()
        {
            throw new System.NotImplementedException();
        }
    }
}