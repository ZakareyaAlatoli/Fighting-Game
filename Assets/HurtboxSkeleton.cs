using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{
    public class HurtboxSkeleton : MonoBehaviour
    {
        public int hits = 0;
        public Hurtbox[] hurtboxes;
        public event System.Action<Hitbox> Struck;
        public List<Hitbox> hitCounter;

        // Start is called before the first frame update
        void Start()
        {
            hitCounter = new List<Hitbox>();
        }

        void OnEnable()
        {
            foreach(Hurtbox h in hurtboxes)
            {
                h.Struck += OnHurt;
            }
        }

        void OnDisable()
        {
            foreach (Hurtbox h in hurtboxes)
            {
                h.Struck -= OnHurt;
            }
        }

        /// <summary>
        /// A hitbox that hits multiple colliders of a single HurtboxSkeleton should
        /// only count as one hit
        /// </summary>
        /// <param name="hitter"></param>
        public void OnHurt(Hitbox hitter)
        {
            if (!hitCounter.Contains(hitter))
            {
                Struck?.Invoke(hitter);
                hits++;
                hitCounter.Add(hitter);
                hitter.Disabled += OnHitboxDisabled;
            }
        }

        private void OnHitboxDisabled(Hitbox hitter)
        {
            hitCounter.Remove(hitter);
            hitter.Disabled -= OnHitboxDisabled;
        }
    }
}