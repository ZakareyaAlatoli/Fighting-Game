using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{
    public class DefaultCharacter : Character
    {
        public Rigidbody worldCollider;
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

        void Start()
        {
            base.Start();
        }
    }
}