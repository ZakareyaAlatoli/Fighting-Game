using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{
    [RequireComponent(typeof(Hurtbox),typeof(HurtboxSkeleton))]
    public class Shield : MonoBehaviour
    {
        private HurtboxSkeleton hbox;
        private Collider coll;
        public Vector3 blockDirection;
        /// <summary>
        /// The value for blocking a hit without aiming the block in any direction (read only)
        /// </summary>
        public static readonly float neutralBlockValue = 2f;

        /// <summary>
        /// A value of 1 means the block faced the hit direction perfectly. -1 means the opposite direction.
        /// </summary>
        public event System.Action<Hitbox, float> Block;
        // Start is called before the first frame update
        void Awake()
        {
            coll = GetComponent<Collider>();
            hbox = GetComponent<HurtboxSkeleton>();
        }

        void OnEnable()
        {
            hbox.Struck += OnHit;
        }

        void OnDisable()
        {
            hbox.Struck -= OnHit;
        }

        public void OnHit(Hitbox hitter)
        {
            Vector3 hitDir = (coll.ClosestPointOnBounds(hitter.transform.position) - transform.position).normalized;
            if (blockDirection == Vector3.zero)
                Block?.Invoke(hitter, neutralBlockValue);
            else
                Block?.Invoke(hitter, Vector3.Dot(blockDirection.normalized, hitDir));
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}