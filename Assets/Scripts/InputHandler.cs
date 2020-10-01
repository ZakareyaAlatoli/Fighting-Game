using Pratfall.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pratfall.Input
{
    public interface IFighterInput
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
        public static readonly string ATTACK = "Attack";
        public static readonly string JUMP = "Jump";
        public static readonly string BLOCK = "Block";

        PlayerInput input;

        public GameObject characterPrefab;
        private Character fighter;

        // Start is called before the first frame update
        void Awake()
        {
            input = GetComponent<PlayerInput>();
            characterPrefab = GameObject.Instantiate(characterPrefab);
            fighter = characterPrefab.GetComponent<Character>();
        }

        void OnEnable()
        {
            input.actions.FindAction(MOVE).performed += OnMove;
            input.actions.FindAction(ATTACK).performed += OnAttack;
            input.actions.FindAction(JUMP).performed += OnJump;
            input.actions.FindAction(BLOCK).performed += OnBlock;
        }

        void OnDisable()
        {
            input.actions.FindAction(MOVE).performed -= OnMove;
            input.actions.FindAction(ATTACK).performed -= OnAttack;
            input.actions.FindAction(JUMP).performed -= OnJump;
            input.actions.FindAction(BLOCK).performed -= OnBlock;
        }

        private void OnMove(InputAction.CallbackContext ctx)
        {
            fighter.OnMove(ctx.ReadValue<Vector2>());
        }

        private void OnAttack(InputAction.CallbackContext ctx)
        {
            fighter.OnAttack(ctx.ReadValue<Vector2>());
        }

        void OnJump(InputAction.CallbackContext ctx)
        {
            fighter.OnJump();
        }

        void OnBlock(InputAction.CallbackContext ctx)
        {
            fighter.OnBlock();
        }
    }
}
