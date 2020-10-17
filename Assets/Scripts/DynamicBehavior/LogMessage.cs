using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall.MoveBehaviors
{
    public class LogMessage : DynamicBehavior
    {
        public string message;
        public override void Perform()
        {
            Debug.Log(message);
        }
    }
}