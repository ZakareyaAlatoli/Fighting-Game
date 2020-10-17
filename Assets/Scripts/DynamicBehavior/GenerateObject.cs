using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall.MoveBehaviors
{
    public class GenerateObject : DynamicBehavior
    {
        public GameObject prefab;
        public GameObject instance { get; protected set; }
        public override void Perform()
        {
            instance = GameObject.Instantiate(prefab);
        }
    }
}