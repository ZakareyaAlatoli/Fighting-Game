using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall.Characters.StandardCharacterMoves
{
    [AddComponentMenu("Pratfall/Attacks/StandardCharacterAttack/StandardCharacter FORWARD AIR")]
    public class atk_StandardChar_FORWARD_AIR : BaseAttack
    {
        [Header("Plug-in Dependencies")]
        public float hitboxDelay;
        public HitboxParameters param;
        public float hitboxOffDelay;
        public float endLag;
        //Private Resources
        Hitbox hitbox;

        //MOVE BEHAVIOR
        protected override IEnumerator Behavior()
        {
            yield return new WaitForSeconds(hitboxDelay);
            hitbox = CreateHitbox(param);

            yield return new WaitForSeconds(hitboxOffDelay);
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