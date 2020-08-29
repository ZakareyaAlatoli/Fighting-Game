using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{
    [RequireComponent(typeof(Collider))]
    public class Hurtbox : MonoBehaviour
    {
        public void OnHit(Hitbox hitter)
        {

        }

        // Start is called before the first frame update
        void Start()
        {
            GetComponent<Collider>().isTrigger = false;
            gameObject.layer = LayerMask.NameToLayer("Hurtbox");
        }
    }
}