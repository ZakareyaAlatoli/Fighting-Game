using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{
    [RequireComponent(typeof(IHittable))]
    public class HitstunController : MonoBehaviour, UI.IHUDString
    {
        private float previousHealth;
        public float health;
        IHittable hittable;

        public string GetDisplayableValue()
        {
            return health.ToString();
        }

        void ProcessHit(HitResult result)
        {
            health -= result.attacker.hitData.damage;
        }

        // Start is called before the first frame update
        void Awake()
        {
            hittable = GetComponent<IHittable>();
        }

        void OnEnable()
        {
            hittable.Hurt += ProcessHit;
        }
        void OnDisable()
        {
            hittable.Hurt -= ProcessHit;
        }
    }
}