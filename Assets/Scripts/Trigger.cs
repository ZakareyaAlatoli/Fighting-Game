﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{
    public interface ITriggerable
    {
        void OnTriggerEntered(Trigger other);
        void OnTriggerStayed(Trigger other);
        void OnTriggerExited(Trigger other);
    }

    [RequireComponent(typeof(Collider))]
    public class Trigger : MonoBehaviour
    {
        public event System.Action<ITriggerable> TriggerStart;
        public event System.Action<ITriggerable> TriggerContinue;
        public event System.Action<ITriggerable> TriggerEnd;
        // Start is called before the first frame update
        void Start()
        {

        }

        protected void OnTriggerEnter(Collider other)
        {
            ITriggerable t = other.GetComponent<ITriggerable>();
            if(t != null)
            {
                t.OnTriggerEntered(this);
                TriggerStart?.Invoke(t);
            }      
        }
        // Update is called once per frame
        protected void OnTriggerStay(Collider other)
        {
            ITriggerable t = other.GetComponent<ITriggerable>();
            if (t != null)
            {
                t.OnTriggerStayed(this);
                TriggerContinue?.Invoke(t);
            }
        }

        protected void OnTriggerExit(Collider other)
        {
            ITriggerable t = other.GetComponent<ITriggerable>();
            if (t != null)
            {
                t.OnTriggerExited(this);
                TriggerEnd?.Invoke(t);
            }
        }
    }
}