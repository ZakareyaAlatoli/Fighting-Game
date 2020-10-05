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
        public static List<PlayerInput> players { get; private set; }       
        PlayerInputManager inputManager;

        void OnPlayerJoined(PlayerInput player)
        {
            player.name = "Player" + inputManager.playerCount.ToString();
            players.Add(player);
            player.transform.SetParent(transform);
        }

        public static void AssignPlayerToControllable(InputHandler handler, IControllable controllable)
        {
            if (handler.controllables == null)
                handler.controllables = new List<IControllable>();
            handler.controllables.Add(controllable);
        }

        void OnPlayerLeft(PlayerInput player)
        {
            players.Remove(player);
            Debug.Log(player.name + " left");
        }

        public static void RemovePlayerFromControllable(InputHandler handler, IControllable controllable)
        {
            handler.controllables.Remove(controllable);
        }

        // Start is called before the first frame update
        void Start()
        {
            inputManager = GetComponent<PlayerInputManager>();
            players = new List<PlayerInput>();
        }
    }
}