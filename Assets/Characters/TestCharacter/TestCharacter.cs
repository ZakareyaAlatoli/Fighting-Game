using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall.Characters
{
    public class TestCharacter : BaseCharacter
    {
        public float jumpForce;
        public float runSpeed;

        public override void OnMove(Vector2 direction)
        {
            worldCollider.AddForce(new Vector2(direction.x * runSpeed, 1f));
        }

        public override void OnAltMove(Vector2 direction) { }

        public override void OnJump()
        {
            worldCollider.AddForce(Vector2.up * jumpForce);
        }

        public override void OnBlock() { }

        public override void OnAttack() { }

        public override void OnSpecial() { }
    }
}