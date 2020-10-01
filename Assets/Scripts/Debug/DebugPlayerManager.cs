using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pratfall.Debugging
{
    public class DebugPlayerManager : MonoBehaviour
    {
        PlayerInputManager inputManager;
        void OnPlayerJoined(PlayerInput player)
        {
            player.name = "Player" + inputManager.playerCount.ToString();
            //player.actions.FindAction("Jump").performed += ctx => Debug.Log(player.name + " jumped!");
        }
        // Start is called before the first frame update
        void Start()
        {
            inputManager = GetComponent<PlayerInputManager>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}