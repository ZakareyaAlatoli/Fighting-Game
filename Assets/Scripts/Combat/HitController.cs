using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{
    /// <summary>
    /// Governs how a character reacts to hits
    /// </summary>
    public class HitController : MonoBehaviour, UI.IHUDString
    {
        public float health;
        public float weight;
        public float hitStun;

        public event System.Action EnteredHitstun, LeftHitstun;

        public Rigidbody body;
        public Shield shield;

        public string GetDisplayableValue() { return health.ToString(); }

        public Hurtbox[] playerHurtboxes;
        public Hurtbox shieldHurtbox;
        [SerializeField]
        private HitTags _hitTags;
        public HitTags hitTags
        {
            get => _hitTags;
            set
            {
                _hitTags = value;
                foreach (Hurtbox h in playerHurtboxes)
                    h.hitTags = _hitTags;
                shieldHurtbox.hitTags = _hitTags;
            }
        }

        Dictionary<GameObject, List<Hitbox>> hitPlayerThisFrame;
        Dictionary<GameObject, List<Hitbox>> hitShieldThisFrame;
        void Awake()
        {        
            hitPlayerThisFrame = new Dictionary<GameObject, List<Hitbox>>();
            hitShieldThisFrame = new Dictionary<GameObject, List<Hitbox>>();
        }

        void Start()
        {
            foreach (Hurtbox h in playerHurtboxes)
            {
                h.hitTags.team = _hitTags.team;
                h.hitTags.origin = _hitTags.origin;
            }
            shieldHurtbox.hitTags.team = _hitTags.team;
            shieldHurtbox.hitTags.origin = _hitTags.origin;
        }

        void ShieldBreakStun()
        {
            //TODO: Get rid of magic numbers
            body.AddForce(Vector3.up * 400f);
            hitStun += 5f;
        }
        void OnEnable()
        {
            foreach (Hurtbox h in playerHurtboxes)
            {
                h.Hurt += OnHurtPlayer;
            }
            shieldHurtbox.Hurt += OnHurtShield;
            shield.ShieldBreak += ShieldBreakStun;
        }
        void OnDisable()
        {
            foreach (Hurtbox h in playerHurtboxes)
            {
                h.Hurt -= OnHurtPlayer;
            }
            shieldHurtbox.Hurt -= OnHurtShield;
            shield.ShieldBreak -= ShieldBreakStun;
        }

        void OnHurtPlayer(HitResult result)
        {
            CollectHitsThisFrame(hitPlayerThisFrame, result);
        }
        void OnHurtShield(HitResult result)
        {
            CollectHitsThisFrame(hitShieldThisFrame, result);
        }
        //Collect all hitboxes that hit any of our hitboxes this frame
        void CollectHitsThisFrame(in Dictionary<GameObject, List<Hitbox>> hitThisFrame, HitResult hitResult)
        {
            if (hitResult.success)
            {
                HitData hitterData = hitResult.attacker.hitData;
                if (!hitThisFrame.ContainsKey(hitterData.hitTags.origin))
                {
                    hitThisFrame.Add(hitterData.hitTags.origin, new List<Hitbox>() { hitResult.attacker });
                }
                else
                {
                    hitThisFrame[hitterData.hitTags.origin].Add(hitResult.attacker);
                }
            }
        }

        Hitbox[] GetSuccessfulHitboxes(Dictionary<GameObject, List<Hitbox>> hitThisFrame)
        {
            List<Hitbox> result = new List<Hitbox>();
            foreach (GameObject source in hitThisFrame.Keys)
            {
                List<Hitbox> hitboxes = hitThisFrame[source];
                int highestPriority = hitboxes[0].hitData.priority;
                Hitbox potentialHitter = hitboxes[0];
                //determine which hitbox from the source has the highest priority...
                foreach (Hitbox hitbox in hitboxes)
                {
                    if (hitbox.hitData.priority > highestPriority)
                    {
                        highestPriority = hitbox.hitData.priority;
                        potentialHitter = hitbox;
                    }
                }
                result.Add(potentialHitter);
            }
            return result.ToArray();
        }

        void ProcessPlayerHits(params Hitbox[] hitboxes)
        {
            foreach(Hitbox h in hitboxes)
            {
                HitBehavior hitReaction = h.hitData.hitBehavior;
                foreach(Hurtbox hurtbox in playerHurtboxes)
                {
                    DisableRehit(h, hurtbox);
                }
                DisableRehit(h, shieldHurtbox);
                //THIS IS WHERE THE MAGIC HAPPENS
                body.AddForce(hitReaction.knockback);
                hitStun += hitReaction.hitStun;
                health -= hitReaction.damage;
                //------------------------------
            }
        }
        void ProcessShieldHits(params Hitbox[] hitboxes)
        {
            foreach(Hitbox h in hitboxes)
            {
                HitBehavior hitReaction = h.hitData.hitBehavior;
                DisableRehit(h, shieldHurtbox);
                foreach (Hurtbox hurtbox in playerHurtboxes)
                {
                    DisableRehit(h, hurtbox);
                }
                //MORE MAGIC HERE
                shield.TakeDamage(hitReaction.shieldDamage);
                //---------------
            }
        }

        bool inHitstun = false;
        void Update()
        {
            float previousHitstun = hitStun;
            hitStun -= Time.deltaTime;
            if(hitStun < 0f)
            {
                hitStun = 0f;
                if (previousHitstun > 0f)
                {
                    inHitstun = false;
                    LeftHitstun?.Invoke();
                }
            }
            else if(hitStun > 0f)
            {
                if (!inHitstun)
                {
                    inHitstun = true;
                    EnteredHitstun?.Invoke();
                }
            }
        }
        void LateUpdate()
        {
            if(hitPlayerThisFrame.Count > 0)
            {
                ProcessPlayerHits(GetSuccessfulHitboxes(hitPlayerThisFrame));
                hitPlayerThisFrame.Clear();
            }
            if (hitShieldThisFrame.Count > 0)
            {
                ProcessShieldHits(GetSuccessfulHitboxes(hitShieldThisFrame));
                hitShieldThisFrame.Clear();
            }
        }

        Coroutine DisableRehit(Hitbox hitbox, Hurtbox hurtbox)
        {
            return StartCoroutine(CR_DisableRehit(hitbox, hurtbox));
        }
        //Prevent the same hitbox from hitting any of our hurtboxes for the specified duration
        private IEnumerator CR_DisableRehit(Hitbox hitbox, Hurtbox hurtbox)
        {
            DisableCollision(hitbox, hurtbox, true);
            if (hitbox.hitData.rehitTime < 0)
                yield break;
            yield return new WaitForSeconds(hitbox.hitData.rehitTime);
            if (hitbox != null)
                DisableCollision(hitbox, hurtbox, false);
        }

        void DisableCollision(Hitbox hitbox, Hurtbox hurtbox, bool ignore)
        {
            foreach(Collider hitboxCollider in hitbox.GetComponents<Collider>())
            {
                Physics.IgnoreCollision(hitboxCollider, hurtbox.attachedCollider, ignore);
                foreach(Hitbox layerHitboxes in hitbox.hitData.layer.hitboxes)
                {
                    foreach(Collider subCollider in layerHitboxes.GetComponents<Collider>())
                    {
                        Physics.IgnoreCollision(subCollider, hurtbox.attachedCollider, ignore);
                    }
                }
            }
        }
    }
}