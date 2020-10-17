using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pratfall.Input;
using Pratfall.Characters;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace Pratfall {
    public class RoundLogic : MonoBehaviour
    {
        public static Dictionary<InputHandler, BaseCharacter> characters;
        public static List<BaseCharacter> spawnedCharacters;
        public UnityEvent UE_RoundStarted;
        public UnityEvent UE_RoundEnded;
        public static event System.Action RoundStarted;
        public static event System.Action RoundEnded;

        public static void QueueCharacter(BaseCharacter characterPrefab, InputHandler playerIndex)
        {
            if (!characters.ContainsKey(playerIndex))
            {
                characters.Add(playerIndex, characterPrefab);
            }
            else
            {
                characters[playerIndex] = characterPrefab;
            }
        }

        public void RoundStart()
        {
            //Spawn each character and give the players control of their selected character
            foreach(InputHandler ih in characters.Keys)
            {
                //Create character
                GameObject instantiatedCharacter = GameObject.Instantiate(characters[ih].gameObject);

                //Maintain list of spawned characters
                spawnedCharacters.Add(instantiatedCharacter.GetComponent<BaseCharacter>());

                //Set team of spawned character
                //TODO: Improve logic for this
                instantiatedCharacter.GetComponent<BaseCharacter>().team = ih.GetHashCode();

                //Give player control of their character
                InputHandler.AssignPlayerToControllable(ih, instantiatedCharacter.GetComponent<BaseCharacter>());
                RoundStarted?.Invoke();
                UE_RoundStarted?.Invoke();
            }
        }

        public void RoundEnd()
        {
            RoundEnded?.Invoke();
            UE_RoundEnded?.Invoke();
        }

        // Start is called before the first frame update
        void Awake()
        {
            characters = new Dictionary<InputHandler, BaseCharacter>();
            spawnedCharacters = new List<BaseCharacter>();
            PlayerSetup.PlayerLeft += PlayerSetup_PlayerLeft;
        }

        void PlayerSetup_PlayerLeft(PlayerInput player)
        {
            characters.Remove(player.GetComponent<InputHandler>());
        }
    }
}