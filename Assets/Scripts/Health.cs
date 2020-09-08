using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall {
    public class Health : MonoBehaviour
    {
        public float health;
        public HurtboxSkeleton hbox;
        /// <summary>
        /// First value is health before change, second value is health after
        /// </summary>
        public event System.Action<float, float> HealthChanged;
        public event System.Action DroppedToZero;
        // Start is called before the first frame update
        void Start()
        {

        }

        public void OnHurt(Hitbox hitter)
        {
            float currentHealth = health;
            health = Mathf.Max(health - hitter.damage, 0f);
            if(health <= 0f && currentHealth > 0f)
            {
                DroppedToZero?.Invoke();
            }
            HealthChanged?.Invoke(currentHealth, health);
        }

        private void OnEnable()
        {
            hbox.Struck += OnHurt;
        }

        private void OnDisable()
        {
            hbox.Struck -= OnHurt;
        }
    }
}