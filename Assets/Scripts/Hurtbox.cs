using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{   
    [RequireComponent(typeof(Collider))]
    public class Hurtbox : MonoBehaviour
    {
        public event System.Action<Hitbox> Struck;

        [Tooltip("Used to decide what hitboxes can affect this hurtbox")]
        public string[] tags;
        public void OnHit(Hitbox hitter)
        {
            Struck?.Invoke(hitter);
        }

        // Start is called before the first frame update
        void Start()
        {
            GetComponent<Collider>().isTrigger = false;
            gameObject.layer = LayerMask.NameToLayer("Hurtbox");
        }
    }
}