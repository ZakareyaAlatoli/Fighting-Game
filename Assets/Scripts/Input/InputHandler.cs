﻿using Pratfall.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pratfall.Input
{
    /// <summary>
    /// Any object that should be driven by player input should implement this
    /// </summary>
    public interface IControllable
    {
        void OnMove(Vector2 direction);
        void OnAltMove(Vector2 direction);
        void OnJump();
        void OnJumpReleased();
        void OnBlock();
        void OnBlockReleased();
        void OnAttack();
        void OnAttackReleased();
        void OnSpecial();
        void OnSpecialReleased();
        void OnStart();
        void OnStartReleased();
    }
    /// <summary>
    /// Passes input from an input device to a controllable object
    /// </summary>
    [RequireComponent(typeof(PlayerInput))]
    public class InputHandler : MonoBehaviour
    {
        public static readonly string MOVE = "Move";
        private InputAction moveAction;
        public static readonly string ALTMOVE = "AltMove";
        private InputAction altMoveAction;
        public static readonly string JUMP = "Jump";
        private InputAction jumpAction;
        public static readonly string BLOCK = "Block";
        private InputAction blockAction;
        public static readonly string ATTACK = "Attack";
        private InputAction attackAction;
        public static readonly string SPECIAL = "Special";
        private InputAction specialAction;
        public static readonly string START = "Start";
        private InputAction startAction;

        PlayerInput input;
        public int port { get => input.playerIndex; }
        /// <summary>
        /// This object passes its inputs to any object that implements IControllable
        /// </summary>
        public List<IControllable> controllables;

        // Start is called before the first frame update
        void Awake()
        {
            if(controllables == null)
                controllables = new List<IControllable>();
            input = GetComponent<PlayerInput>();

            moveAction = input.actions.FindAction(MOVE);
            altMoveAction = input.actions.FindAction(ALTMOVE);
            jumpAction = input.actions.FindAction(JUMP);
            blockAction = input.actions.FindAction(BLOCK);
            attackAction = input.actions.FindAction(ATTACK);
            specialAction = input.actions.FindAction(SPECIAL);
            startAction = input.actions.FindAction(START);
        }

        void OnEnable()
        {
            jumpAction.performed += OnJump;
            jumpAction.canceled += OnJumpReleased;

            blockAction.performed += OnBlock;
            blockAction.canceled += OnBlockReleased;

            attackAction.performed += OnAttack;
            attackAction.canceled += OnAttackReleased;

            specialAction.performed += OnSpecial;
            specialAction.canceled += OnSpecialReleased;

            startAction.performed += OnStart;
            startAction.canceled += OnStartReleased;
        }

        void OnDisable()
        {
            jumpAction.performed -= OnJump;
            jumpAction.canceled -= OnJumpReleased;

            blockAction.performed -= OnBlock;
            blockAction.canceled -= OnBlockReleased;

            attackAction.performed -= OnAttack;
            attackAction.canceled -= OnAttackReleased;

            specialAction.performed -= OnSpecial;
            specialAction.canceled -= OnSpecialReleased;

            startAction.performed -= OnStart;
            startAction.canceled -= OnStartReleased;
        }


        void OnJump(InputAction.CallbackContext ctx)
        {
            foreach (IControllable controllable in controllables)
            {
                controllable.OnJump();
            }
        }
        void OnJumpReleased(InputAction.CallbackContext ctx)
        {
            foreach (IControllable controllable in controllables)
            {
                controllable.OnJumpReleased();
            }
        }

        void OnBlock(InputAction.CallbackContext ctx)
        {
            foreach (IControllable controllable in controllables)
            {
                controllable.OnBlock();
            }
        }
        void OnBlockReleased(InputAction.CallbackContext ctx)
        {
            foreach (IControllable controllable in controllables)
            {
                controllable.OnBlockReleased();
            }
        }

        void OnAttack(InputAction.CallbackContext ctx)
        {
            foreach (IControllable controllable in controllables)
            {
                controllable.OnAttack();
            }
        }
        void OnAttackReleased(InputAction.CallbackContext ctx)
        {
            foreach (IControllable controllable in controllables)
            {
                controllable.OnAttackReleased();
            }
        }

        void OnSpecial(InputAction.CallbackContext ctx)
        {
            foreach (IControllable controllable in controllables)
            {
                controllable.OnSpecial();
            }
        }
        void OnSpecialReleased(InputAction.CallbackContext ctx)
        {
            foreach (IControllable controllable in controllables)
            {
                controllable.OnSpecialReleased();
            }
        }

        void OnStart(InputAction.CallbackContext ctx)
        {
            foreach (IControllable controllable in controllables)
            {
                controllable.OnStart();
            }
        }
        void OnStartReleased(InputAction.CallbackContext ctx)
        {
            foreach (IControllable controllable in controllables)
            {
                controllable.OnStartReleased();
            }
        }

        void Update()
        {
            foreach (IControllable controllable in controllables)
            {
                if(controllable == null)
                {
                    RemovePlayerFromControllable(this, controllable);
                    continue;
                }
                controllable.OnMove(moveAction.ReadValue<Vector2>());
                controllable.OnAltMove(altMoveAction.ReadValue<Vector2>());
            }
        }

        class MissingControllableComponentException : System.Exception { }

        public static void AssignPlayerToControllable(InputHandler handler, IControllable controllable)
        {
            if (handler.controllables == null)
                handler.controllables = new List<IControllable>();
            if (controllable == null)
                throw new MissingControllableComponentException();
            else
            {
                handler.controllables.Add(controllable);
            }    
        }

        public static void RemovePlayerFromControllable(InputHandler handler, IControllable controllable)
        {
            handler.controllables.Remove(controllable);
        }
    }
}
