using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall.Characters.StandardCharacterMoves
{
    [AddComponentMenu("Pratfall/Attacks/StandardCharacterAttack/StandardCharacter NEUTRAL AIR")]
    public class atk_StandardChar_NEUTRAL_AIR : BaseAttack
    {
        protected override IEnumerator Behavior()
        {
            Debug.Log("NEUTRAL AIR");
            yield return new WaitForSeconds(0.2f);
        }

        protected override void OnFinished() { }

        protected override void OnInterrupted() { }
    }
}