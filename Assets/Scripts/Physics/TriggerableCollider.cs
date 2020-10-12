using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{
    [RequireComponent(typeof(Collider))]
    public class TriggerableCollider : MonoBehaviour, ITriggerable
    {
        public Collider associatedCollider { get; private set; }
        public event System.Action<Trigger> TriggerEntered;
        public event System.Action<Trigger> TriggerStayed;
        public event System.Action<Trigger> TriggerExited;

        public void OnTriggerEntered(Trigger other) { TriggerEntered?.Invoke(other); }
        public void OnTriggerStayed(Trigger other) { TriggerStayed?.Invoke(other); }
        public void OnTriggerExited(Trigger other) { TriggerExited?.Invoke(other); }

        void Awake() { associatedCollider = GetComponent<Collider>(); }
    }
}
