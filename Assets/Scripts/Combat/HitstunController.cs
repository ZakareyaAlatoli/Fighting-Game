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

        public event Action<string> HUDStringChanged;

        public string GetDisplayableValue()
        {
            return health.ToString();
        }

        void ProcessHit(HitResult result)
        {
            Debug.Log($"Hit by {result.attacker.name}");
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

        // Update is called once per frame
        void Update()
        {
            if(previousHealth != health)
            {
                HUDStringChanged?.Invoke(GetDisplayableValue());
                previousHealth = health;
            }
        }
    }
}