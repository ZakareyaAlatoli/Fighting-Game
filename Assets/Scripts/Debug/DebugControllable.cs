using Pratfall.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall.Debugging
{  
    public class DebugControllable : MonoBehaviour, IControllable
    {
        public void OnMove(Vector2 direction) { Debug.Log($"Move Vector: {direction}"); }

        public void OnAltMove(Vector2 direction) { Debug.Log($"Alt Move Vector: {direction}"); }

        public void OnJump() { Debug.Log($"JUMPED"); }

        public void OnJumpReleased() { Debug.Log($"JUMP RELEASED"); }

        public void OnBlock() { Debug.Log($"BLOCKED"); }

        public void OnAttack() { Debug.Log($"ATTACKED"); }

        public void OnSpecial() { Debug.Log($"SPECIALED"); }

        public void OnBlockReleased()
        {
            throw new System.NotImplementedException();
        }

        public void OnAttackReleased()
        {
            
        }

        public void OnSpecialReleased()
        {
         
        }
    }
}