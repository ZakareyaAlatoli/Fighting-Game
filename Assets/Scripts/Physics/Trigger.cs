using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{
    public interface ITriggerable
    {
        void OnTriggerEntered(Trigger other);
        void OnTriggerStayed(Trigger other);
        void OnTriggerExited(Trigger other);
        Collider attachedCollider { get; }
    }

    [RequireComponent(typeof(Collider))]
    public class Trigger : MonoBehaviour
    {
        Collider _attachedCollider;
        public event System.Action<ITriggerable> TriggerStart;
        public event System.Action<ITriggerable> TriggerContinue;
        public event System.Action<ITriggerable> TriggerEnd;
        // Start is called before the first frame update
        void Awake()
        {
            _attachedCollider = GetComponent<Collider>();
            _attachedCollider.isTrigger = true;
        }
        void OnEnable()
        {
            _attachedCollider.enabled = true;
        }
        void OnDisable()
        {
            _attachedCollider.enabled = false;
        }

        protected void OnTriggerEnter(Collider other)
        {
            ITriggerable t = other.GetComponent<ITriggerable>();
            if(t != null)
            {
                if (enabled)
                {
                    t.OnTriggerEntered(this);
                    TriggerStart?.Invoke(t);
                }
            }      
        }
        // Update is called once per frame
        protected void OnTriggerStay(Collider other)
        {
            ITriggerable t = other.GetComponent<ITriggerable>();
            if (t != null)
            {
                if (enabled)
                {
                    t.OnTriggerStayed(this);
                    TriggerContinue?.Invoke(t);
                }
            }
        }

        protected void OnTriggerExit(Collider other)
        {
            ITriggerable t = other.GetComponent<ITriggerable>();
            if (t != null)
            {
                if (enabled)
                {
                    t.OnTriggerExited(this);
                    TriggerEnd?.Invoke(t);
                }
            }
        }
    }
}