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