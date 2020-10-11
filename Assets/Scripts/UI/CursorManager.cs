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

        void Awake()
        {
            //Cursors should display over the rest of the UI
            HUD = FindObjectOfType<Canvas>();
            PlayerSetup.PlayerJoined += PlayerSetup_PlayerJoined;
            PlayerSetup.PlayerLeft += PlayerSetup_PlayerLeft;
            spawnedCursors = new Dictionary<InputHandler, GameObject>();
        }

        //When a player leaves, their cursor should be destroyed
        void PlayerSetup_PlayerLeft(PlayerInput player)
        {
            InputHandler handler = player.GetComponent<InputHandler>();
            InputHandler.RemovePlayerFromControllable(handler, spawnedCursors[handler].GetComponent<Input.Cursor>());
            Destroy(spawnedCursors[handler]);
            spawnedCursors.Remove(handler);
        }

        void OnEnable()
        {
            foreach (InputHandler ih in spawnedCursors.Keys)
                spawnedCursors[ih].SetActive(true);
        }

        void OnDisable()
        {
            foreach(InputHandler ih in spawnedCursors.Keys)
                spawnedCursors[ih].SetActive(false);
        }
        //When a player joins, create for them a cursor
        void PlayerSetup_PlayerJoined(PlayerInput player)
        {
            InputHandler handler = player.GetComponent<InputHandler>();
            GameObject cursor = Instantiate(cursorPrefab);

            Input.Cursor cursorData = cursor.GetComponent<Input.Cursor>();
            cursorData.associatedPlayer = handler;
            cursorData.header = "P" + (player.playerIndex + 1);

            cursor.name = "CursorP" + (player.playerIndex + 1);
            cursor.transform.SetParent(HUD.transform);
            spawnedCursors.Add(handler, cursor);
            InputHandler.AssignPlayerToControllable(handler, cursor.GetComponent<Input.Cursor>());
            if (!enabled)
                cursor.SetActive(false);
        }
    }
}