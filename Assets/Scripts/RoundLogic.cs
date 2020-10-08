using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pratfall.Input;
using Pratfall.Characters;
using UnityEngine.InputSystem;

namespace Pratfall {
    public class RoundLogic : MonoBehaviour
    {
        public static Dictionary<InputHandler, Character> characters;

        public static void QueueCharacter(Character characterPrefab, InputHandler playerIndex)
        {
            if (!characters.ContainsKey(playerIndex))
            {
                characters.Add(playerIndex, characterPrefab);
            }
            else
            {
                characters[playerIndex] = characterPrefab;
            }

            foreach (InputHandler ih in characters.Keys)
            {
                Debug.Log("Player" + (ih.GetComponent<PlayerInput>().playerIndex + 1) + ": " + characters[ih]);
            }
        }

        public void RoundStart()
        {
            foreach(InputHandler ih in characters.Keys)
            {
                PlayerSetup.AssignPlayerToControllable(ih, characters[ih]);
            }
        }

        public void RoundEnd()
        {
            //
        }

        void OnEnable()
        {
            //RoundStart();
        }

        // Start is called before the first frame update
        void Awake()
        {
            characters = new Dictionary<InputHandler, Character>();
            PlayerSetup.PlayerLeft += PlayerSetup_PlayerLeft;
        }

        void PlayerSetup_PlayerLeft(PlayerInput player)
        {
            characters.Remove(player.GetComponent<InputHandler>());
            Debug.Log("Player" + (player.playerIndex + 1) + " left");
            if(characters.Count <= 0)
            {
                Debug.Log("NO MORE CHARACTERS BEEYOTCH");
            }
            else
            {
                foreach (InputHandler ih in characters.Keys)
                {
                    Debug.Log("Player" + (ih.GetComponent<PlayerInput>().playerIndex + 1) + ": " + characters[ih]);
                }
            }
        }
    }
}