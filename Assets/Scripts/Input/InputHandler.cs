using Pratfall.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pratfall.Input
{
    public interface IControllable
    {
        void OnMove(Vector2 direction);
        void OnAttack(Vector2 direction);
        void OnJump();
        void OnBlock();
    }

    [RequireComponent(typeof(PlayerInput))]
    public class InputHandler : MonoBehaviour
    {
        public static readonly string MOVE = "Move";
        private InputAction moveAction;
        public static readonly string ATTACK = "Attack";
        private InputAction attackAction;
        public static readonly string JUMP = "Jump";
        private InputAction jumpAction;
        public static readonly string BLOCK = "Block";
        private InputAction blockAction;

        PlayerInput input;
        /// <summary>
        /// This object passes its inputs to any object that implements IControllable
        /// </summary>
        public IControllable[] controllables;

        // Start is called before the first frame update
        void Awake()
        {
            controllables = new IControllable[] { };
            input = GetComponent<PlayerInput>();

            moveAction = input.actions.FindAction(MOVE);
            attackAction = input.actions.FindAction(ATTACK);
            jumpAction = input.actions.FindAction(JUMP);
            blockAction = input.actions.FindAction(BLOCK);
        }

        void OnEnable()
        {
            jumpAction.performed += OnJump;
            blockAction.performed += OnBlock;
        }

        void OnDisable()
        {
            jumpAction.performed -= OnJump;
            blockAction.performed -= OnBlock;
        }


        void OnJump(InputAction.CallbackContext ctx)
        {
            foreach (IControllable controllable in controllables)
            {
                controllable.OnJump();
            }
        }

        void OnBlock(InputAction.CallbackContext ctx)
        {
            foreach (IControllable controllable in controllables)
            {
                controllable.OnBlock();
            }
        }

        void Update()
        {
            foreach(IControllable controllable in controllables)
            {
                controllable.OnMove(moveAction.ReadValue<Vector2>());
                controllable.OnAttack(attackAction.ReadValue<Vector2>());
            }
        }
    }
}
