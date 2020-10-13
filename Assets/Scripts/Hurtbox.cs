using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{
    [RequireComponent(typeof(TriggerableCollider))]
    public class Hurtbox : MonoBehaviour, IHittable
    {
        private Collider _attachedCollider;
        public Collider attachedCollider { get => _attachedCollider; }
        public HitFlags resistances;
        public event System.Action<HitResult> Hurt;

        void Start()
        {
            _attachedCollider = GetComponent<TriggerableCollider>().attachedCollider;
        }

        public void OnHurt(Hitbox hitter)
        {
            HitFlags pierces = hitter.hitData.ignore;
            HitFlags blocked = ((pierces | resistances) ^ pierces);

            HitResult result = new HitResult()
            {
                attacker = hitter,
                blocked = blocked
            };
            //ORs the attacker's hitflags with the defender's hitflags then XORs the result
            //with the attacker's hitflags. If this yields 0, then the attack goes through
            if (((pierces | resistances) ^ pierces) == 0)
                result.success = true;
            else
                result.success = false;
            Hurt?.Invoke(result);
        }
    }
}