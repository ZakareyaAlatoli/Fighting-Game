using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{
    [System.Flags]
    public enum HitFlags
    {
        None = 0,
        Shielded = 1,
        Intangible = 2
    }
    [System.Serializable]
    public class HitTags
    {
        public int team;
        public GameObject origin;

        public static bool operator==(HitTags lval, HitTags rval)
        {
            if (lval.origin != rval.origin)
                return false;
            if (lval.team != rval.team)
                return false;
            return true;
        }

        public static bool operator !=(HitTags lval, HitTags rval)
        {
            if (lval.origin == rval.origin)
                return false;
            if (lval.team == rval.team)
                return false;
            return true;
        }
    }

    public interface IHittable
    {
        void OnHurt(Hitbox hitter);
        event System.Action<HitResult> Hurt;
        HitTags hitTags { get; set; }
    }

    public struct HitResult
    {
        public bool success;
        public Hitbox attacker;
        public HitFlags blocked;
    }

    [System.Serializable]
    public struct HitBehavior
    {
        public float damage;
        public Vector2 knockback;
        public float hitStun;
    }

    [System.Serializable]
    public struct HitData
    {
        /// <summary>
        /// The object that spawned this hitbox (usually a character)
        /// </summary>
        [Tooltip("The object the hitboxes \"belong\" to. Hitboxes from different origins are processed independently")]
        [HideInInspector]
        public HitTags hitTags;
        /// <summary>
        /// Hitboxes in the same layer won't hit the same hurtbox before the rehit time if one of them hits
        /// </summary>
        [Tooltip("Hitboxes in the same layer can't hit a hurtbox one of them has already hit before the rehit time is up")]
        [HideInInspector]
        public HitboxLayer layer;
        public HitBehavior hitBehavior;
        /// <summary>
        /// When a hurtbox is struck by multiple hitboxes in the same frame with the same origin
        /// the highest priority one goes through
        /// </summary>
        [Tooltip("If hitboxes in the same layer hit the same hurtbox in the same frame, the highest priority one takes precedence")]
        public int priority;
        public int rehitTime;
        public bool damageSelf;
        public bool damageTeammates;
        public HitFlags ignore;
    }

    [RequireComponent(typeof(Trigger))]
    public class Hitbox : MonoBehaviour
    {
        public HitData hitData;
        private Trigger trigger;  

        void Awake()
        {
            trigger = GetComponent<Trigger>();
            gameObject.layer = LayerMask.NameToLayer("Hitbox");
        }

        void OnEnable()
        {
            trigger.TriggerContinue += OnTriggerContinue;
        }

        void OnTriggerContinue(ITriggerable obj)
        {
            IHittable hittable = obj.attachedCollider.GetComponent<IHittable>();
            if (gameObject.activeSelf)
            {
                if (hittable != null)
                {
                    hittable.OnHurt(this);
                }
            }
        }
    }
}