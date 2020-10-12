using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{
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
    }
    [RequireComponent(typeof(Trigger))]
    public class Hitbox : MonoBehaviour
    {
        public HitData hitData;

        void Awake()
        {
            gameObject.layer = LayerMask.NameToLayer("Hitbox");
        }
    }
}