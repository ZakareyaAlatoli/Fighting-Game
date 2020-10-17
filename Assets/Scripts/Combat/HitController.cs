using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{
    /// <summary>
    /// Governs how a character reacts to hits
    /// </summary>
    [RequireComponent(typeof(HurtboxModel))]
    public class HitController : MonoBehaviour, UI.IHUDString
    {
        public Rigidbody rb;
        public float hitStunTimer { get; private set; }
        public float health;
        HurtboxModel hittable;

        public event System.Action EnteredHitstun;
        public event System.Action LeftHitstun;

        public string GetDisplayableValue()
        {
            return health.ToString();
        }

        float previousHitStun;
        void ProcessHit(HitResult result)
        {
            HitBehavior hit = result.attacker.hitData.hitBehavior;
            health -= hit.damage;
            hitStunTimer += hit.hitStun;
            if (previousHitStun <= 0f)
                EnteredHitstun?.Invoke();
            previousHitStun = hitStunTimer;
            rb.AddForce(hit.knockback);
        }

        void Awake()
        {
            if (GetComponent<UI.HUDString>() == null)
                gameObject.AddComponent<UI.HUDString>();
            hittable = GetComponent<HurtboxModel>();
        }

        void OnEnable()
        {
            hittable.Hurt += ProcessHit;
        }
        void OnDisable()
        {
            hittable.Hurt -= ProcessHit;
        }

        void Update()
        {
            hitStunTimer = Mathf.Max(hitStunTimer - Time.deltaTime, 0f);
            if(previousHitStun > 0f && hitStunTimer <= 0f)
            {
                LeftHitstun?.Invoke();
            }
            previousHitStun = hitStunTimer;
        }
    }
}