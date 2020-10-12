using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{
    public class CharacterHealth : MonoBehaviour
    {
        public Health health;
        public Hurtbox hurtbox;

        void OnHurt(Hitbox hitter)
        {
            health.value -= hitter.hitData.damage;
        }

        void OnEnable()
        {
            hurtbox.Hurt += OnHurt;
        }

        void OnDisable()
        {
            hurtbox.Hurt -= OnHurt;
        }
    }
}