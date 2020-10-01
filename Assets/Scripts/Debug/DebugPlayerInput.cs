using Pratfall.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pratfall.Debugging
{
    [RequireComponent(typeof(PlayerInput))]
    public class DebugPlayerInput : MonoBehaviour
    {
        PlayerInput input;
        // Start is called before the first frame update
        void Start()
        {
            input = GetComponent<PlayerInput>();
            input.actions.FindAction("Jump").performed += OnJump;
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnJump(InputAction.CallbackContext ctx)
        {
            Debug.Log(name + " jumped!");  
        }

        private void OnMove(InputAction.CallbackContext vec)
        {
            //
        }
    }
}