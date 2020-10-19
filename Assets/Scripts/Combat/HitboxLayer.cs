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
        public event System.Action<HurtboxModel> HitCallback;
        public List<Hitbox> hitboxes { get; private set; }
        public void Add(Hitbox item)
        {
            hitboxes.Add(item);
            item.Hit += OnHit;
        }
        void OnDisable()
        {
            foreach(Hitbox h in hitboxes)
            {
                h.Hit -= OnHit;
            }
        }
        void OnHit(HurtboxModel target)
        {
            HitCallback?.Invoke(target);
        }

    }
}