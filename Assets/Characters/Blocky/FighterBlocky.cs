using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall.Characters
{
    public class FighterBlocky : BaseCharacter
    {
        public float speed;

        public override void OnMove(Vector2 direction)
        {
            worldCollider.AddForce(direction * speed);
        }

        public override void OnAltMove(Vector2 direction) { }

        public override void OnJump() { }
        public override void OnJumpReleased() { }

        public override void OnBlock() { }
        public override void OnBlockReleased() { }

        public override void OnAttack() { }
        public override void OnAttackReleased() { }

        public override void OnSpecial() { }
        public override void OnSpecialReleased() { }

        public override void OnStart() { }
        public override void OnStartReleased() { }
    }
}