using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall.MoveBehaviors
{
    public class RemoveObject : DynamicBehavior
    {
        public GenerateObject objectGenerator;
        public override void Perform()
        {
            if (objectGenerator.instance != null)
                Destroy(objectGenerator.instance);
        }
    }
}