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
        void Awake() {
            midAction = false;
            interrupted = false;
        }

        protected virtual void Start()
        {
            //
        }

        protected virtual void Update()
        {
            
        }

        public bool midAction { get; private set; }
        private bool interrupted;
        private Coroutine coroutine;

        public event System.Action<DynamicAction> Started;
        protected void InvokeStart() { Started?.Invoke(this); }
        public event System.Action<DynamicAction> StartedMidAction;
        protected void InvokeStartMidAction() { StartedMidAction?.Invoke(this); }
        public event System.Action<DynamicAction> StartFailed;
        protected void InvokeStartFailed() { StartFailed?.Invoke(this); }
        public void Begin() {
            interrupted = false;
            if (enabled)
            {
                if (midAction)
                {
                    OnStartMidAction();
                    StartedMidAction?.Invoke(this);
                }
                else {
                    OnStart();
                    Started?.Invoke(this);
                    midAction = true;
                    StartCoroutine(CR_Begin());
                }
            }
            else
            {
                OnFailedStart();
                StartFailed?.Invoke(this);
            }  
        }

        public event System.Action<DynamicAction> FullyCompleted;
        protected void InvokeFullyCompleted() { FullyCompleted?.Invoke(this); }
        public event System.Action<DynamicAction> Ended;
        protected void InvokeEnded() { Ended?.Invoke(this); }
        private IEnumerator CR_Begin()
        {
            yield return coroutine = StartCoroutine(Behavior());
            if (!interrupted)
            {
                OnCompleted();
                FullyCompleted?.Invoke(this);
            }
            OnFinished();
            Ended?.Invoke(this);
            midAction = false;
            interrupted = false;
        }

        public event System.Action<DynamicAction> Interrupted;
        protected void InvokeInterrupted() { Interrupted?.Invoke(this); }
        public void Stop()
        {
            if (midAction)
            {
                interrupted = true;
                StopCoroutine(coroutine);
                midAction = false;
                OnInterrupted();
                Interrupted?.Invoke(this);
            }
        }
        protected virtual IEnumerator Behavior() { yield return null; }
        protected virtual void OnStart() { }
        protected virtual void OnFailedStart() { }
        protected virtual void OnStartMidAction() { }
        protected virtual void OnInterrupted() { }
        protected virtual void OnCompleted() { }
        protected virtual void OnFinished() { }
    }
}