using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall.MoveBehaviors
{
    public class RemoveHitbox : DynamicBehavior
    {
        public GenerateHitbox[] hitboxes;
        public override void Perform()
        {
            foreach(GenerateHitbox generator in hitboxes)
            {
                foreach (GameObject g in generator.hitboxInstances)
                {
                    Destroy(g);
                }
                generator.hitboxInstances.Clear();
                Destroy(generator.hitboxMaster);
            }
        }
    }
}