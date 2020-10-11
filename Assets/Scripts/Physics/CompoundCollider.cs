using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{
    public class CompoundCollider : TriggerableCollider
    {
        public TriggerableCollider[] subColliders;
        public new event System.Action<Trigger> TriggerEntered;
        public new event System.Action<Trigger> TriggerStayed;
        public new event System.Action<Trigger> TriggerExited;

        private void AddTrigger(Trigger other)
        {
            if (overlappingTriggers.ContainsKey(other))
            {
                overlappingTriggers[other]++;
            }
            else
            {
                overlappingTriggers.Add(other, 1);
                TriggerEntered?.Invoke(other);
            }
        }

        private void RemoveTrigger(Trigger other)
        {
            if (overlappingTriggers.ContainsKey(other))
            {
                int current = --overlappingTriggers[other];
                if (current == 0)
                {
                    TriggerExited?.Invoke(other);
                    overlappingTriggers.Remove(other);
                }
            }
        }

        private List<Trigger> triggeredThisFrame;
        private bool clearingTriggers = false;
        private void ContinueTrigger(Trigger other)
        {
            foreach(Trigger t in overlappingTriggers.Keys)
            {
                if (!triggeredThisFrame.Contains(t))
                {
                    triggeredThisFrame.Add(t);
                    TriggerStayed?.Invoke(t);
                }
            }
            if (!clearingTriggers)
            {
                StartCoroutine(CR_ClearTriggered());
            }
        }

        private IEnumerator CR_ClearTriggered()
        {
            yield return new WaitForEndOfFrame();
            triggeredThisFrame.Clear();
            clearingTriggers = false;
        }

        private Dictionary<Trigger, int> overlappingTriggers;

        void OnEnable()
        {
            foreach(TriggerableCollider t in subColliders)
            {
                t.TriggerEntered += AddTrigger;
                t.TriggerStayed += ContinueTrigger;
                t.TriggerExited += RemoveTrigger;
            }
        }

        void OnDisable()
        {
            foreach (TriggerableCollider t in subColliders)
            {
                t.TriggerEntered -= AddTrigger;
                t.TriggerStayed -= ContinueTrigger;
                t.TriggerExited -= RemoveTrigger;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            overlappingTriggers = new Dictionary<Trigger, int>();
            triggeredThisFrame = new List<Trigger>();
        }
    }
}