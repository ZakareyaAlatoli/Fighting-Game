using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{
    /// <summary>
    /// Fighters' attacks should inherit from this. Behavior is called from Begin().
    /// OnInterrupted() is called when the move is stopped before it would naturally
    /// finish. OnFinished is called when the move runs till completion. If the move
    /// creates any entities, they should be cleaned up when it ends or when appropriate
    /// </summary>
    public abstract class BaseAttack : DynamicAction
    {
        /// <summary>
        /// The character using this move. You can get information about
        /// their state to change how a move works
        /// </summary>
        public Characters.BaseCharacter user;
        protected override abstract IEnumerator Behavior();
        protected override abstract void OnFinished();
        protected abstract override void OnInterrupted();

        //Helper functions
        protected Hitbox CreateHitbox(bool startEnabled, float damage, Vector2 knockback, bool damageSelf, int priority, float rehitTime)
        {
            GameObject hitbox = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            //DO STUFF WITH HITBOX
            hitbox.GetComponent<Renderer>().material = GlobalResource.HitboxMaterial;
            Hitbox hitInfo = hitbox.AddComponent<Hitbox>();
            hitInfo.hitData.hitTags = new HitTags { origin = user.gameObject, team = user.team };

            if (!startEnabled) DisableHitbox(hitInfo);

            hitInfo.hitData.hitBehavior.damage = damage;
            hitInfo.hitData.hitBehavior.knockback = knockback;
            hitInfo.hitData.damageSelf = damageSelf;
            hitInfo.hitData.priority = priority;
            hitInfo.hitData.rehitTime = rehitTime;
            //-------------------
            return hitInfo;
        }

        protected void RemoveHitbox(params Hitbox[] items)
        {
            foreach (Hitbox h in items)
                Destroy(h.gameObject);
        }

        protected void EnableHitbox(Hitbox item)
        {
            item.enabled = true;
            Material mat = item.GetComponent<Renderer>().material;
            if (mat == null)
                return;
            Color enabledColor = mat.color;
            enabledColor.a = 0.25f;
            mat.color = enabledColor;
        }

        protected void DisableHitbox(Hitbox item)
        {
            item.enabled = false;
            Material mat = item.GetComponent<Renderer>().material;
            if (mat == null)
                return;
            Color enabledColor = mat.color;
            enabledColor.a = 0.5f;
            mat.color = enabledColor;
        }

        protected Coroutine FollowObject(Transform follower, Transform followee)
        {
            return StartCoroutine(CR_FollowObject(follower, followee));
        }

        protected IEnumerator CR_FollowObject(Transform follower, Transform followee)
        {
            while (follower != null && followee != null)
            {
                follower.position = followee.position;
                yield return null;
            }
        }
    }
}