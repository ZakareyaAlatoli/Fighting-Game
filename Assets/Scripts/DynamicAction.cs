using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{
    /// <summary>
    /// Player actions that take place over some amount of time rather than being momentary
    /// should derive from this class
    /// </summary>
    public abstract class DynamicAction : MonoBehaviour {
        protected void Start() {
            midAction = false;
            interrupted = false;
            Precache();
        }

        protected virtual void Update()
        {
            
        }

        private bool midAction;
        private bool interrupted;
        protected Coroutine coroutine { get; private set; }
        public void Begin() {
            interrupted = false;
            if (enabled)
            {
                if (midAction)
                    OnStartMidAction();
                else {
                    midAction = true;
                    StartCoroutine(CR_Begin());
                }
            }
            else
                OnFailedStart();
        }

        protected IEnumerator CR_Begin()
        {
            yield return coroutine = StartCoroutine(Behavior());
            if (!interrupted)
                OnCompleted();
            midAction = false;
            interrupted = false;
            OnFinished();
        }

        public void Stop()
        {
            if (midAction)
            {
                interrupted = true;
                StopCoroutine(coroutine);
                midAction = false;
                OnInterrupted();
            }
        }
        protected virtual IEnumerator Behavior() { yield return null; }
        protected virtual void OnFailedStart() { }
        protected virtual void OnStartMidAction() { }
        protected virtual void OnInterrupted() { }
        protected virtual void OnCompleted() { }
        protected virtual void OnFinished() { }
        /// <summary>
        /// Called after Start()
        /// </summary>
        protected virtual void Precache() { }
    }
}