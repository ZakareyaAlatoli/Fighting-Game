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
    }
}