using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall.Characters.StandardCharacterMoves
{
    [AddComponentMenu("Pratfall/Attacks/StandardCharacterAttack/StandardCharacter JAB")]
    public class atk_StandardChar_JAB : BaseAttack
    {
        [Header("Plug-in Dependencies")]
        public float hitboxDelay = 0.1f;
        public HitboxParameters param;
        public float endLag = 0.5f;
        //Private Resources
        Hitbox hitbox;

        //MOVE BEHAVIOR
        protected override IEnumerator Behavior()
        {
            yield return new WaitForSeconds(hitboxDelay);
            hitbox = CreateHitbox(param);
            //END LAG
            yield return new WaitForSeconds(endLag);
        }
        //END MOVE BEHAVIOR
        protected override void OnInterrupted()
        {
            RemoveHitbox(hitbox);
        }

        protected override void OnFinished()
        {
            RemoveHitbox(hitbox);
        }
    }
}