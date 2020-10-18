using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall.Characters
{
    public class FighterGallal : BaseCharacter
    {
        public float speed;
        [Header("Moveset")]
        public AttackAction GALLAL_PUNCH;

        public override void OnMove(Vector2 direction)
        {
            Vector3 final = new Vector3(direction.x, direction.y, 0f);
            worldCollider.AddForce(final * speed);
        }

        public override void OnAltMove(Vector2 direction) { }

        public override void OnJump() { }

        public override void OnBlock() { }

        public override void OnAttack()
        {
            GALLAL_PUNCH.Begin();
        }

        public override void OnSpecial() { }
    }
}