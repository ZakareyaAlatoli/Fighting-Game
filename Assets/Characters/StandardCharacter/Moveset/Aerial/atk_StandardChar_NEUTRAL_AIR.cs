using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall.Characters.StandardCharacterMoves
{
    [AddComponentMenu("Pratfall/Attacks/StandardCharacterAttack/StandardCharacter NEUTRAL AIR")]
    public class atk_StandardChar_NEUTRAL_AIR : BaseAttack
    {
        //Private Resources
        Hitbox hitbox;

        //MOVE BEHAVIOR
        protected override IEnumerator Behavior()
        {
            yield return new WaitForSeconds(0.1f);
            hitbox = CreateHitbox(true, 2f, new Vector2(300f, 300f), false, 0, -1f);
            hitbox.transform.localScale *= 2.0f;
            FollowObject(hitbox.transform, user.worldCollider.transform);

            yield return new WaitForSeconds(0.2f);
            DisableHitbox(hitbox);

            //END LAG
            yield return new WaitForSeconds(0.3f);
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