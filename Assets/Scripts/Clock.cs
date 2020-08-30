using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pratfall
{
    [System.Serializable]
    public class Timer
    {
        public float currentTime;
        public float maxTime;
        public event System.Action TimeUp;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        public void Tick()
        {
            if (currentTime > 0.0f)
            {
                currentTime -= Time.deltaTime;
                if (currentTime <= 0.0f)
                    TimeUp?.Invoke();
            }
        }
    }

    public class Clock : MonoBehaviour
    {
        public Timer time;
        public Text display;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            time.Tick();
            System.TimeSpan span = System.TimeSpan.FromSeconds(time.currentTime);
            display.text = span.ToString("hh':'mm':'ss");
        }
    }
}