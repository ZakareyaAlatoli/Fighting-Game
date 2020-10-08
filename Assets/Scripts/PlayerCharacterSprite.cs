using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Pratfall
{
    [RequireComponent(typeof(Image))]
    public class PlayerCharacterSprite : MonoBehaviour
    {
        public int slot = 0;
        private Image characterSprite;
        // Start is called before the first frame update
        void Start() { characterSprite = GetComponent<Image>(); }

        void OnEnable() { CharacterSelector.CharacterSelected += CharacterSelector_CharacterSelected; }

        void Disable() { CharacterSelector.CharacterSelected += CharacterSelector_CharacterSelected; }

        void CharacterSelector_CharacterSelected(PlayerInput player, Characters.Character character)
        {
            if(player.playerIndex == slot)
            {
                characterSprite.sprite = character.sprite;
            }
        }
    }
}