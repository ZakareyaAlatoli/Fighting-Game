using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Pratfall.Input
{
    public class InputEventController : MonoBehaviour, IControllable
    {
        private InputHandler previous;
        public InputHandler controller;
        public UnityEvent Jump, Block, Attack, Special;

        public void OnAltMove(Vector2 direction) { }

        public void OnMove(Vector2 direction) { }

        public void OnAttack() { Jump?.Invoke(); }

        public void OnBlock() { Block?.Invoke(); }

        public void OnJump() { Jump?.Invoke(); }

        public void OnSpecial() { Special?.Invoke(); }

        void OnValidate()
        {
            if(previous != controller)
            {
                if(previous != null)
                    InputHandler.RemovePlayerFromControllable(previous, this);
                if(controller != null)
                    InputHandler.AssignPlayerToControllable(controller, this);
                previous = controller;
            }
        }
    }
}