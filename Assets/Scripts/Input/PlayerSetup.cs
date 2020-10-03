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
        public static List<PlayerInput> players { get; private set; }       
        PlayerInputManager inputManager;
        

        void OnPlayerJoined(PlayerInput player)
        {
            player.name = "Player" + inputManager.playerCount.ToString();
            players.Add(player);
            player.transform.SetParent(transform);
            Debug.Log(player.name + " joined");
        }

        void OnPlayerLeft(PlayerInput player)
        {
            players.Remove(player);
            Debug.Log(player.name + " left");
        }

        // Start is called before the first frame update
        void Start()
        {
            inputManager = GetComponent<PlayerInputManager>();
            players = new List<PlayerInput>();
        }
    }
}