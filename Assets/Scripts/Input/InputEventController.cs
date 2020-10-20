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
        [Header("Jump")]
        public UnityEvent Jump, JumpReleased; 

        [Header("Block")]
        public UnityEvent Block, BlockReleased;

        [Header("Attack")]
        public UnityEvent Attack, AttackReleased;

        [Header("Special")]
        public UnityEvent Special, SpecialReleased;

        [Header("Start")]
        public UnityEvent Start, StartReleased;

        public void OnAltMove(Vector2 direction) { }

        public void OnMove(Vector2 direction) { }

        public void OnJump() { Jump?.Invoke(); }
        public void OnJumpReleased() { JumpReleased?.Invoke(); }

        public void OnAttack() { Attack?.Invoke(); }
        public void OnAttackReleased() { AttackReleased?.Invoke(); }

        public void OnBlock() { Block?.Invoke(); }
        public void OnBlockReleased() { BlockReleased?.Invoke(); }

        public void OnSpecial() { Special?.Invoke(); }
        public void OnSpecialReleased() { SpecialReleased?.Invoke(); }

        public void OnStart() { Start?.Invoke(); }
        public void OnStartReleased() { StartReleased?.Invoke(); }


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