using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall.Characters.StandardCharacterMoves
{
    [AddComponentMenu("Pratfall/Attacks/StandardCharacterAttack/StandardCharacter DOWN TILT")]
    public class atk_StandardChar_DOWN_TILT : BaseAttack
    {
        [Header("Plug-in Dependencies")]
        public HitboxParameters hitboxParam;
        public float hitboxDelay = 0.1f;
        public float hitboxDuration = 0.1f;
        public float endLag = 0.3f;

        //Private Resources
        Hitbox hitbox;

        //MOVE BEHAVIOR
        protected override IEnumerator Behavior()
        {
            yield return new WaitForSeconds(hitboxDelay);
            hitbox = CreateHitbox(hitboxParam);

            yield return new WaitForSeconds(hitboxDuration);
            DisableHitbox(hitbox);

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