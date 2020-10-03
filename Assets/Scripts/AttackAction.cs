using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{
    public class AttackAction : DynamicAction
    {
        public DynamicBehavior[] timeline;

        protected override IEnumerator Behavior()
        {
            foreach (DynamicBehavior timestamp in timeline)
            {
                yield return new WaitForSeconds(timestamp.time);
                timestamp.Perform();
            }
        }

        protected override void OnFinished()
        {
            foreach(DynamicBehavior timestamp in timeline)
            {
                timestamp.Undo();
            }
        }
    }
    /// <summary>
    /// Specifies what events should happen over the duration of a move
    /// </summary>
    [System.Serializable]
    public class DynamicBehavior
    {
        /// <summary>
        /// Time to wait since the last dynamic behavior/ beginning of the move execution
        /// </summary>
        public float time;
        public string message;
        public void Perform()
        {
            Debug.Log(message);
        }
        public void Undo()
        {
            //
        }
    }
}