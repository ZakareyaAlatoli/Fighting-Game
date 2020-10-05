using Pratfall.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        private void PlayerSetup_PlayerLeft(InputHandler obj, int port)
        {
            PlayerSetup.RemovePlayerFromControllable(obj, spawnedCursors[obj].GetComponent<Input.Cursor>());
            Destroy(spawnedCursors[obj]);
            spawnedCursors.Remove(obj);
        }

        void PlayerSetup_PlayerJoined(InputHandler obj, int port)
        {
            GameObject cursor = Instantiate(cursorPrefab);
            cursor.name = "CursorP" + (port + 1).ToString();
            cursor.transform.SetParent(HUD.transform);
            spawnedCursors.Add(obj, cursor);
            PlayerSetup.AssignPlayerToControllable(obj, cursor.GetComponent<Input.Cursor>());
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}