using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{
    public class HitEvent
    {
        public Hitbox hitter;
        public Hurtbox victim;
    }

    [RequireComponent(typeof(Collider))]
    public class Hitbox : MonoBehaviour
    {
        public static event System.Action<HitEvent> Hit;
        public float damage;
        public Filter<string> filter;
        public float rehitTimer;
 
        // Start is called before the first frame update
        void Start()
        {
            GetComponent<Collider>().isTrigger = true;
            gameObject.layer = LayerMask.NameToLayer("Hitbox");
        }

        void OnTriggerStay(Collider other)
        {
            Hurtbox hbox = other.GetComponent<Hurtbox>();
            if(hbox != null)
            {
                if (filter.Check(hbox.tags))
                {
                    hbox.OnHit(this);
                    Hit?.Invoke(new HitEvent { hitter = this, victim = hbox} );
                }
            }
        }
    }
}