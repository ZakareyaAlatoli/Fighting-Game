using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{
    [DisallowMultipleComponent]
    public class AttackAction : DynamicAction
    {
        private DynamicBehavior[] timeline;
        void Awake()
        {
            timeline = GetComponents<DynamicBehavior>();
        }

        protected override IEnumerator Behavior()
        {
            foreach (DynamicBehavior timestamp in timeline)
            {
                yield return new WaitForSeconds(timestamp.delay);
                timestamp.Perform();
            }
        }

        public event System.Action<AttackAction> Started;
        protected override void OnStart()
        {
            Started?.Invoke(this);
        }
        public event System.Action<AttackAction> Interrupted;
        protected override void OnInterrupted()
        {
            timeline[timeline.Length - 1].Perform();
            Interrupted?.Invoke(this);
        }
        public event System.Action<AttackAction> Completed;
        protected override void OnFinished()
        {
            timeline[timeline.Length - 1].Perform();
            Completed?.Invoke(this);
        }
    }

    /// <summary>
    /// Specifies what events should happen over the duration of a move
    /// </summary>
    [System.Serializable]
    public abstract class DynamicBehavior : MonoBehaviour
    {
        public float delay;
        public abstract void Perform();
    }
}