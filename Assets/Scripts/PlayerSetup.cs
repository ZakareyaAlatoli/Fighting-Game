using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pratfall.Input
{
    /// <summary>
    /// Manages game state for players joining/leaving
    /// </summary>
    [RequireComponent(typeof(PlayerInputManager))]
    public class PlayerSetup : MonoBehaviour
    {
        PlayerInputManager inputManager;
        

        void OnPlayerJoined(PlayerInput player)
        {
            player.name = "Player" + inputManager.playerCount.ToString();
        }
        // Start is called before the first frame update
        void Start()
        {
            inputManager = GetComponent<PlayerInputManager>();
        }
    }
}