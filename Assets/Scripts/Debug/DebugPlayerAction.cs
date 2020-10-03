using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pratfall.Debugging
{
    public class DebugPlayerAction : MonoBehaviour
    {
        public DynamicAction playerAction;
        // Start is called before the first frame update
        void Start(){ }

        // Update is called once per frame
        void Update() {
            if (Keyboard.current.qKey.wasPressedThisFrame && playerAction != null)
                playerAction.Begin();
            if (Keyboard.current.wKey.wasPressedThisFrame && playerAction != null)
                playerAction.Stop();
        }
    }
}