using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{
    /// <summary>
    /// Shields hurtboxes
    /// </summary>
    public class Shield : MonoBehaviour
    {
        public Trigger hurtboxShielder;
        public Hurtbox shieldBubble;
        Renderer shieldVisual;
        List<Hurtbox> shieldedObjects;
        float defaultSize;

        public System.Action ShieldOn, ShieldOff;

        public float shrinkSpeed;
        public float regrowSpeed;
        void Awake()
        {
            hurtboxShielder.gameObject.layer = LayerMask.NameToLayer("Hitbox");
            shieldVisual = shieldBubble.GetComponent<Renderer>();
            shieldedObjects = new List<Hurtbox>();
            defaultSize = transform.localScale.x;

            hurtboxShielder.TriggerStart += OnTriggered;
            hurtboxShielder.TriggerEnd += OnStoppedTrigger;
            //BUG: For some reason the hurtboxes don't become shielded
            //the first you shield, this is a temporary fix
            TurnOff();
            TurnOn();
            TurnOff();
        }
        void OnEnable() { }
        void OnDisable() { }
        void OnDestroy()
        {
            hurtboxShielder.TriggerStart -= OnTriggered;
            hurtboxShielder.TriggerEnd -= OnStoppedTrigger;
        }

        Coroutine sizeChange;
        public void TurnOn()
        {
            ShieldOn?.Invoke();
            enabled = true;
            hurtboxShielder.enabled = true;
            shieldBubble.enabled = true;
            shieldVisual.enabled = true;
            if(sizeChange != null)
                StopCoroutine(sizeChange);
            sizeChange = ChangeSize(transform, 0.1f, shrinkSpeed * Time.deltaTime);
        }
        public void TurnOff()
        {
            ShieldOff?.Invoke();
            enabled = false;
            hurtboxShielder.enabled = false;
            shieldBubble.enabled = false;
            shieldVisual.enabled = false;
            foreach(Hurtbox h in shieldedObjects)
            {
                h.resistances ^= HitFlags.Shielded;
            }
            shieldedObjects.Clear();
            if(sizeChange != null)
                StopCoroutine(sizeChange);
            sizeChange = ChangeSize(transform, defaultSize, regrowSpeed * Time.deltaTime);

        }
        void SetSize(float normalizedSize)
        {
            normalizedSize = Mathf.Max(normalizedSize, 0.1f);
            transform.localScale = Vector3.one * (normalizedSize * defaultSize);
        }
        public void TakeDamage(float damage)
        {
            SetSize((transform.localScale.x / defaultSize) - (damage / defaultSize));
        }
        void OnTriggered(ITriggerable triggerable)
        {
            Hurtbox triggeredHurtbox = triggerable.attachedCollider.GetComponent<Hurtbox>();
            //If the shield started overlapping a hurtbox
            if (triggeredHurtbox != null && triggeredHurtbox != shieldBubble)
            {
                //and the hurtbox belongs to the same character
                if (triggeredHurtbox.hitTags.Matches(shieldBubble.hitTags))
                {
                    //set the hurtbox hitflags to shielded
                    shieldedObjects.Add(triggeredHurtbox);
                    triggeredHurtbox.resistances |= HitFlags.Shielded;
                }
            }
        }
        void OnStoppedTrigger(ITriggerable triggerable)
        {
            Hurtbox triggeredHurtbox = triggerable.attachedCollider.GetComponent<Hurtbox>();
            if (triggeredHurtbox != null && triggeredHurtbox != shieldBubble)
            {
                if(triggeredHurtbox.hitTags.Matches(shieldBubble.hitTags))
                {
                    shieldedObjects.Remove(triggeredHurtbox);
                    triggeredHurtbox.resistances = triggeredHurtbox.resistances ^ HitFlags.Shielded;
                }
            }
        }
        Coroutine ChangeSize(Transform target, float desiredScale, float rate)
        {
            return StartCoroutine(CR_ChangeSize(target, desiredScale, rate));
        }
        IEnumerator CR_ChangeSize(Transform target, float desiredScale, float rate)
        {
            Vector3 vectorScale = Vector3.one * desiredScale;
            Vector3 scaleRate = Vector3.one * rate;
            //Shrink
            if(target.localScale.x > desiredScale)
            {
                while(target.localScale.x > desiredScale)
                {
                    target.localScale -= scaleRate;
                    if (target.localScale.x < desiredScale)
                    {
                        target.localScale = vectorScale;
                        yield break;
                    }
                    yield return null;
                }
            }
            //Grow
            else
            {
                while (target.localScale.x < desiredScale)
                {
                    target.localScale += scaleRate;
                    if (target.localScale.x > desiredScale)
                    {
                        target.localScale = vectorScale;
                        yield break;
                    }
                    yield return null;
                }
            }
        }
    }
}