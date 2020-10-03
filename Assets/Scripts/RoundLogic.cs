using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pratfall.Input;
using Pratfall.Characters;

namespace Pratfall {
    public class RoundLogic : MonoBehaviour
    {
        public IControllable[] characters;
        public void RoundStart()
        {
            for (int i = 0; i < PlayerSetup.players.Count; i++)
            {
                PlayerSetup.players[i].GetComponent<InputHandler>().controllables =
                new IControllable[] { FindObjectOfType<Character>() as IControllable };

                AssignPlayerToCharacter(PlayerSetup.players[i].GetComponent<InputHandler>(), characters[i]);
            }  
        }

        public void RoundEnd()
        {
            //
        }

        void AssignPlayerToCharacter(InputHandler player, IControllable character)
        {
            //
        }

        // Start is called before the first frame update
        void Start()
        {
            RoundStart();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}