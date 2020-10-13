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
        Armored = 2,
        Invincible = 4,
        Intangible = 8
    }

    public interface IHittable
    {
        void OnHurt(Hitbox hitter);
    }

    public struct HitResult
    {
        public bool success;
        public Hitbox attacker;
        public HitFlags blocked;
    }

    [System.Serializable]
    public struct HitData
    {
        /// <summary>
        /// The object that spawned this hitbox (usually a character)
        /// </summary>
        [Tooltip("The object the hitboxes \"belong\" to. Hitboxes from different origins are processed independently")]
        public GameObject origin;
        /// <summary>
        /// Hitboxes in the same layer won't hit the same hurtbox before the rehit time if one of them hits
        /// </summary>
        [Tooltip("Hitboxes in the same layer can't hit a hurtbox one of them has already hit before the rehit time is up")]
        public Hitbox[] layer;
        public float damage;
        /// <summary>
        /// When a hurtbox is struck by multiple hitboxes in the same frame with the same origin
        /// the highest priority one goes through
        /// </summary>
        [Tooltip("If hitboxes in the same layer hit the same hurtbox in the same frame, the highest priority one takes precedence")]
        public int priority;
        public int rehitTime;
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
            if (hittable != null)
                hittable.OnHurt(this);
        }
    }
}