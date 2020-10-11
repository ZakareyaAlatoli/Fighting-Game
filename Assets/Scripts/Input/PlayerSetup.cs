using Pratfall.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pratfall
{
    /// <summary>
    /// Manages game state for players joining/leaving
    /// </summary>
    [RequireComponent(typeof(PlayerInputManager))]
    public class PlayerSetup : MonoBehaviour
    {
        /// <summary>
        /// List of all currently active players
        /// </summary>
        public static List<PlayerInput> players { get; private set; }

        public static event System.Action<PlayerInput> PlayerJoined;
        public static event System.Action<PlayerInput> PlayerLeft;
        PlayerInputManager inputManager;

        void OnPlayerJoined(PlayerInput player)
        {
            player.name = "Player" + (player.playerIndex + 1);
            players.Add(player);
            player.transform.SetParent(transform);
            PlayerJoined?.Invoke(player);
        }

        void OnPlayerLeft(PlayerInput player)
        {
            PlayerLeft?.Invoke(player);
            players.Remove(player);
        }

        // Start is called before the first frame update
        void Awake()
        {
            inputManager = GetComponent<PlayerInputManager>();
            players = new List<PlayerInput>();
        }
    }
}