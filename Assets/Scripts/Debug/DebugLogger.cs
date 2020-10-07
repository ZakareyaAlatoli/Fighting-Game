using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall.Debugging
{
    public class DebugLogger : MonoBehaviour
    {
        public void Print(string message)
        {
            Debug.Log(message);
        }
        // Start is called before the first frame update
        void Start() { }
    }
}