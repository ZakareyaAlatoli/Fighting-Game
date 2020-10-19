using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall.Characters.StandardCharacterMoves
{
    [AddComponentMenu("Pratfall/Attacks/StandardCharacterAttack/StandardCharacter BACK AIR")]
    public class atk_StandardChar_BACK_AIR : BaseAttack
    {
        protected override IEnumerator Behavior()
        {
            Debug.Log("BACK AIR");
            yield return new WaitForSeconds(0.2f);
        }

        protected override void OnFinished() { }

        protected override void OnInterrupted() { }
    }
}