using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall.Characters.StandardCharacterMoves
{
    [AddComponentMenu("Pratfall/Attacks/StandardCharacterAttack/StandardCharacter FORWARD AIR")]
    public class atk_StandardChar_FORWARD_AIR : BaseAttack
    {
        protected override IEnumerator Behavior()
        {
            Debug.Log("FORWARD AIR");
            yield return new WaitForSeconds(0.2f);
        }

        protected override void OnFinished() { }

        protected override void OnInterrupted() { }
    }
}