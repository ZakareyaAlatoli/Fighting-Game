using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall {
    /// <summary>
    /// Groups hitboxes together. Hitboxes in the same layer cannot hit the same hurtbox
    /// at the same time.
    /// </summary>
    public class HitboxLayer
    {
        public HitboxLayer()
        {
            hitboxes = new List<Hitbox>();
        }
        public HitboxLayer(params Hitbox[] items)
        {
            hitboxes = new List<Hitbox>();
            foreach(Hitbox h in items)
            {
                Add(h);
            }
        }

        public List<Hitbox> hitboxes { get; private set; }
        public void Add(Hitbox item)
        {
            hitboxes.Add(item);
            item.hitData.layer = this;

        }
    }
}