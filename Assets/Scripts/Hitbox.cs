using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{
    [RequireComponent(typeof(Collider))]
    public class Hitbox : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            GetComponent<Collider>().isTrigger = true;
            gameObject.layer = LayerMask.NameToLayer("Hitbox");
        }

        private void OnTriggerEnter(Collider other)
        {
            GetComponent<Hurtbox>()?.OnHit(this);
        }
    }
}