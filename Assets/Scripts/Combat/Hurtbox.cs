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
        [SerializeField]
        private HitTags _hitTags;
        public HitTags hitTags
        {
            get => _hitTags;
            set => _hitTags = value;
        }

        public HitFlags resistances;
        public event System.Action<HitResult> Hurt;

        void Start()
        {
            gameObject.layer = LayerMask.NameToLayer("Hurtbox");
            _attachedCollider = GetComponent<TriggerableCollider>().attachedCollider;
        }

        public static bool attackPass(HitFlags attacker, HitFlags defender)
        {
            if (((attacker | defender) ^ attacker) == 0)
                return true;
            else
                return false;
        }

        public void OnHurt(Hitbox hitter)
        {
            HitData hitData = hitter.hitData;
            HitFlags pierces = hitter.hitData.ignore;
            HitFlags blocked = ((pierces | resistances) ^ pierces);

            HitResult result = new HitResult()
            {
                attacker = hitter,
                blocked = blocked
            };
            //If the hitbox has the same origin as me...
            if(hitData.hitTags.origin == _hitTags.origin)
            {
                //and self-damages...
                if (hitData.damageSelf)
                {
                    if(attackPass(hitData.ignore, resistances))
                        result.success = true;
                    else
                        result.success = false;
                    Hurt?.Invoke(result);
                }
            }
            //If the hitbox has a different origin than me...
            else
            {
                //and it's from a teammate and it hits friendlies...
                if((hitData.hitTags.team == _hitTags.team) && hitData.damageTeammates)
                {
                    if (attackPass(hitData.ignore, resistances))
                        result.success = true;
                    else
                        result.success = false;
                    Hurt?.Invoke(result);
                }
                //or if it's from an opponent
                else
                {
                    if (attackPass(hitData.ignore, resistances))
                        result.success = true;
                    else
                        result.success = false;
                    Hurt?.Invoke(result);
                }
            }
        }
    }
}