using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pratfall.UI;
using System;

namespace Pratfall
{
    public class Health : MonoBehaviour, IHUDString
    {
        public float value;
        public HurtboxModel hurtbox;
        private float previousHealth;
        public event Action<string> HUDStringChanged;

        public string GetDisplayableValue()
        {
            return value.ToString();
        }

        // Start is called before the first frame update
        void Start()
        {
            previousHealth = value + 1;
            hurtbox.Hurt += Hurtbox_Hurt;
        }

        private void Hurtbox_Hurt(Hitbox obj)
        {
            value -= obj.hitData.damage;
        }

        // Update is called once per frame
        void Update()
        {
            if (value != previousHealth)
            {
                HUDStringChanged?.Invoke(GetDisplayableValue());
                previousHealth = value;
            }     
        }
    }
}