using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{
    /// <summary>
    /// Filters hitboxes that can hit this object
    /// </summary>
    public class HurtboxModel : MonoBehaviour
    {
        public Hurtbox[] hurtboxes;

        void Awake()
        {
            hitboxesHitThisFrame = new List<Hitbox>();
            hitThisFrame = new Dictionary<GameObject, List<Hitbox>>();
        }

        void OnEnable()
        {
            foreach(Hurtbox h in hurtboxes)
            {
                h.Hurt += TallyHitsThisFrame;
            }
        }
        void OnDisable()
        {
            foreach (Hurtbox h in hurtboxes)
            {
                h.Hurt -= TallyHitsThisFrame;
            }
        }

        List<Hitbox> hitboxesHitThisFrame;

        void TallyHitsThisFrame(HitResult hitResult)
        {
            if (hitResult.success)
            {
                if (!hitboxesHitThisFrame.Contains(hitResult.attacker))
                {
                    hitboxesHitThisFrame.Add(hitResult.attacker);
                    ProcessHitboxes(hitResult.attacker);
                }
            }
        }

        /// <summary>
        /// Fired when this hurtbox is hurt
        /// </summary>
        public event System.Action<Hitbox> Hurt;

        private bool processingHitboxes = false;
        private Dictionary<GameObject, List<Hitbox>> hitThisFrame;
        //Determine which hitbox takes precedence
        void ProcessHitboxes(Hitbox hitter)
        {
            GameObject source = hitter.hitData.origin;
            //If we got hit by a hitbox from a new source
            if (!hitThisFrame.ContainsKey(source))
                hitThisFrame.Add(source, new List<Hitbox>() { hitter });
            //If we got hit by a hitbox from the same source as another hitbox that hit us this frame...
            else
            {
                //and it's a new hitbox
                if (!hitThisFrame[source].Contains(hitter))
                {
                    hitThisFrame[source].Add(hitter);
                }
            }
            if (!processingHitboxes)
            {
                processingHitboxes = true;
                StartCoroutine(DetermineHits());
            }
        }

        private IEnumerator DetermineHits()
        {
            //At the end of the frame after we've been hit by hitboxes...
            yield return new WaitForEndOfFrame();
            //for each individual source of hitboxes that hit us this frame...
            foreach(GameObject source in hitThisFrame.Keys)
            {
                List<Hitbox> hitboxes = hitThisFrame[source];
                int highestPriority = hitboxes[0].hitData.priority;
                Hitbox potentialHitter = hitboxes[0];
                //determine which hitbox from the source has the highest priority...
                foreach(Hitbox hitbox in hitboxes)
                {
                    if(hitbox.hitData.priority > highestPriority)
                    {
                        highestPriority = hitbox.hitData.priority;
                        potentialHitter = hitbox;
                    }
                }
                //and hit us with it
                ReceiveHit(potentialHitter);
            }
            hitThisFrame.Clear();
            hitboxesHitThisFrame.Clear();
            processingHitboxes = false;
        }

        private IEnumerator DisableRehit(Hitbox hitbox)
        {
            DisableCollision(hitbox, true);
            if (hitbox.hitData.rehitTime < 0)
                yield break;
            yield return new WaitForSeconds(hitbox.hitData.rehitTime);
            DisableCollision(hitbox, false);
        }

        void DisableCollision(Hitbox hitbox, bool ignore)
        {
            //For each subcollider in this hurtbox's compound collider...
            foreach (Hurtbox subCollider in hurtboxes)
            {
                //disable collisions between it and this hitbox's colliders...
                foreach (Collider hitboxCollider in hitbox.GetComponents<Collider>())
                    Physics.IgnoreCollision(subCollider.attachedCollider, hitboxCollider, ignore);
                    
                //and collisions between it and the hitbox's associated hitboxes' colliders
                foreach (Hitbox associatedHitbox in hitbox.hitData.layer)
                {
                    foreach (Collider hitboxCollider in associatedHitbox.GetComponents<Collider>())
                        Physics.IgnoreCollision(subCollider.attachedCollider, hitboxCollider, ignore);
                }
            }
        }

        void ReceiveHit(Hitbox hitter)
        {
            StartCoroutine(DisableRehit(hitter));
            Hurt?.Invoke(hitter);
        }
    }
}