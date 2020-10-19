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

        public bool midAction { get; private set; }
        private bool interrupted;
        private Coroutine coroutine;

        public event System.Action Started;
        protected void InvokeStart() { Started?.Invoke(); }
        public event System.Action StartedMidAction;
        protected void InvokeStartMidAction() { StartedMidAction?.Invoke(); }
        public event System.Action StartFailed;
        protected void InvokeStartFailed() { StartFailed?.Invoke(); }
        public void Begin() {
            interrupted = false;
            if (enabled)
            {
                if (midAction)
                {
                    OnStartMidAction();
                    StartedMidAction?.Invoke();
                }
                else {
                    OnStart();
                    Started?.Invoke();
                    midAction = true;
                    StartCoroutine(CR_Begin());
                }
            }
            else
            {
                OnFailedStart();
                StartFailed?.Invoke();
            }  
        }

        public event System.Action FullyCompleted;
        protected void InvokeFullyCompleted() { FullyCompleted?.Invoke(); }
        public event System.Action Ended;
        protected void InvokeEnded() { Ended?.Invoke(); }
        private IEnumerator CR_Begin()
        {
            yield return coroutine = StartCoroutine(Behavior());
            if (!interrupted)
            {
                OnCompleted();
                FullyCompleted?.Invoke();
            }
            OnFinished();
            Ended?.Invoke();
            midAction = false;
            interrupted = false;
        }

        public event System.Action Interrupted;
        protected void InvokeInterrupted() { Interrupted?.Invoke(); }
        public void Stop()
        {
            if (midAction)
            {
                interrupted = true;
                StopCoroutine(coroutine);
                midAction = false;
                OnInterrupted();
                Interrupted?.Invoke();
            }
        }
        protected virtual IEnumerator Behavior() { yield return null; }
        protected virtual void OnStart() { }
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