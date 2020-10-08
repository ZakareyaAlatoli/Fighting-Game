using Pratfall.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pratfall {
    public class CursorManager : MonoBehaviour
    {
        private Canvas HUD;
        public GameObject cursorPrefab;
        private Dictionary<InputHandler, GameObject> spawnedCursors;
        // Start is called before the first frame update
        void Start()
        {
            HUD = FindObjectOfType<Canvas>();
            PlayerSetup.PlayerJoined += PlayerSetup_PlayerJoined;
            PlayerSetup.PlayerLeft += PlayerSetup_PlayerLeft;
            spawnedCursors = new Dictionary<InputHandler, GameObject>();
        }

        private void PlayerSetup_PlayerLeft(PlayerInput player)
        {
            InputHandler handler = player.GetComponent<InputHandler>();
            PlayerSetup.RemovePlayerFromControllable(handler, spawnedCursors[handler].GetComponent<Input.Cursor>());
            Destroy(spawnedCursors[handler]);
            spawnedCursors.Remove(handler);
        }

        void PlayerSetup_PlayerJoined(PlayerInput player)
        {
            InputHandler handler = player.GetComponent<InputHandler>();
            GameObject cursor = Instantiate(cursorPrefab);
            cursor.GetComponent<Input.Cursor>().associatedPlayer = handler;
            cursor.name = "CursorP" + (player.playerIndex + 1).ToString();
            cursor.transform.SetParent(HUD.transform);
            spawnedCursors.Add(handler, cursor);
            PlayerSetup.AssignPlayerToControllable(handler, cursor.GetComponent<Input.Cursor>());
        }
    }
}