using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall.Characters.StandardCharacterMoves
{
    [AddComponentMenu("Pratfall/Attacks/StandardCharacterAttack/StandardCharacter BACK AIR")]
    public class atk_StandardChar_BACK_AIR : BaseAttack
    {
        //Private Resources
        Hitbox hitbox;

        //MOVE BEHAVIOR
        protected override IEnumerator Behavior()
        {
            yield return new WaitForSeconds(0.1f);
            hitbox = CreateHitbox(true, 2f, new Vector2(-1000f, 500f), false, 0, -1f);
            hitbox.transform.localScale *= 1.5f;
            FollowObject(hitbox.transform, user.worldCollider.transform, new Vector3(-0.5f, 0f, 0f));

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