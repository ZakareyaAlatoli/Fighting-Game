using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall {

    public class HitboxLayer : MonoBehaviour
    {
        void Awake()
        {
            hitboxes = new List<Hitbox>();
        }
        public List<Hitbox> hitboxes;
    }
}