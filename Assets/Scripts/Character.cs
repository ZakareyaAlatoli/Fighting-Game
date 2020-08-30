using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pratfall
{
    public class Character : MonoBehaviour
    {
        public PlayerInput input;

        public void Fire(InputAction.CallbackContext context)
        {
            Debug.Log("Fire!");
        }
        // Start is called before the first frame update
        void Start()
        {
            input = GetComponent<PlayerInput>();
        }

        void OnEnable()
        {
            input.onActionTriggered += ctx => Debug.Log(name + " did something");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}