using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall.MoveBehaviors
{
    public class EnableHitbox : DynamicBehavior
    {
        public GenerateHitbox generator;
        public int[] id;
        public override void Perform()
        {
            foreach(int i in id)
            {
                if (i >= 0 && i < generator.hitboxes.Length)
                {
                    foreach (Hitbox h in generator.hitboxes[i].hitboxData)
                    {
                        h.gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}