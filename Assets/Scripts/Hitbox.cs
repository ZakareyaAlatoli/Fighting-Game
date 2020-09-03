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
        public event System.Action<Hitbox> Disabled;
        public Filter<string> filter;
 
        // Start is called before the first frame update
        void Start()
        {
            GetComponent<Collider>().isTrigger = true;
            gameObject.layer = LayerMask.NameToLayer("Hitbox");
        }

        private void OnTriggerEnter(Collider other)
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
        void OnDisable()
        {
            Disabled?.Invoke(this);
        }
    }
}