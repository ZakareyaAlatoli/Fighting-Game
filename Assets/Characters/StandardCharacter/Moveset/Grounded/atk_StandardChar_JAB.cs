using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall.Characters.StandardCharacterMoves
{
    [AddComponentMenu("Pratfall/Attacks/StandardCharacterAttack/StandardCharacter JAB")]
    public class atk_StandardChar_JAB : BaseAttack
    {
        [Header("Plug-in Dependencies")]
        public Transform rightHand;
        //Private Resources
        Hitbox hitbox;

        //MOVE BEHAVIOR
        protected override IEnumerator Behavior()
        {
            yield return new WaitForSeconds(0.1f);
            hitbox = CreateHitbox(true, 2f, new Vector2(-400f, 200f), false, 0, -1f);
            FollowObject(hitbox.transform, rightHand);
            //END LAG
            yield return new WaitForSeconds(0.5f);
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