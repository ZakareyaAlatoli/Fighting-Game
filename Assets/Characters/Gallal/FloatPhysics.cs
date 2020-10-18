using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{
    public class FloatPhysics : PhysicsModifier
    {
        public float airResistance;
        protected override void FixedUpdate()
        {
            body.velocity = Vector3.Lerp(body.velocity, Vector3.zero, airResistance);
        }
    }
}