using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pratfall {
    public class DisplayHealth : MonoBehaviour
    {
        public Text text;
        public Health health;
        // Start is called before the first frame update
        void Start()
        {

        }

        public void OnHealthChanged(float old, float newer)
        {
            text.text = "HP: " + newer.ToString();
        }

        void OnEnable()
        {
            health.HealthChanged += OnHealthChanged;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}