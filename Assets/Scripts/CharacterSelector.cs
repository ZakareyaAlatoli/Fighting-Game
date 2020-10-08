using Pratfall.Characters;
using Pratfall.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Pratfall {
    [RequireComponent(typeof(Button))]
    public class CharacterSelector : MonoBehaviour, ISelectableByPlayer
    {
        public static event System.Action<PlayerInput, Character> CharacterSelected;
        public Character characerPrefab;
        private Button button;

        // Start is called before the first frame update
        void Start()
        {
            button = GetComponent<Button>();
        }

        public void OnSelectedByPlayer(InputHandler playerIndex)
        {
            RoundLogic.QueueCharacter(characerPrefab, playerIndex);
            CharacterSelected?.Invoke(playerIndex.GetComponent<PlayerInput>(), characerPrefab);
        }
    }
}
