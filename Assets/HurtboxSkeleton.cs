using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{
    public class HurtboxSkeleton : MonoBehaviour
    {
        public Hurtbox[] hurtboxes;
        public event System.Action<Hitbox> Struck;

        // Start is called before the first frame update
        void Start()
        {
            hitThisFrame = new List<Hitbox>();
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

        private List<Hitbox> hitThisFrame;
        private IEnumerator CR_ResetHitFlag()
        {
            yield return new WaitForEndOfFrame();
            hitThisFrame.Clear();
        }

        private IEnumerator CR_TriggerTimeout(Hitbox hitter)
        {
            foreach(Hurtbox h in hurtboxes)
            {
                Physics.IgnoreCollision(h.GetComponent<Collider>(), hitter.GetComponent<Collider>(), true);
            }
            yield return new WaitForSeconds(hitter.rehitTimer);
            foreach (Hurtbox h in hurtboxes)
            {
                Physics.IgnoreCollision(h.GetComponent<Collider>(), hitter.GetComponent<Collider>(), false);
            }
        }

        public void OnHurt(Hitbox hitter)
        {
            //Prevent the same hitbox from hitting multiple parts of the skeleton in one frame
            if (!hitThisFrame.Contains(hitter))
            {
                Struck?.Invoke(hitter);
                StartCoroutine(CR_TriggerTimeout(hitter));
                hitThisFrame.Add(hitter);
                StartCoroutine(CR_ResetHitFlag());
            }
        }
    }
}