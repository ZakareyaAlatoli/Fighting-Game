using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall.Characters.StandardCharacterMoves
{
    [AddComponentMenu("Pratfall/Attacks/StandardCharacterAttack/StandardCharacter DOWN TILT")]
    public class atk_StandardChar_DOWN_TILT : BaseAttack
    {
        [Header("Plug-in Dependencies")]
        public float hitboxDelay = 0.1f;

        public bool hitboxInitiallyOn = true;
        public float damage = 2f;
        public Vector2 knockback;
        public bool damageSelf = false;
        public int priority = 0;
        public float rehitTime = -1f;

        public Transform hitboxAttachmentPoint;
        public Vector3 hitboxOffset = new Vector3(0.5f, -0.3f, 0f);

        public float hitboxDuration = 0.1f;
        public float endLag = 0.3f;



        //Private Resources
        Hitbox hitbox;

        //MOVE BEHAVIOR
        protected override IEnumerator Behavior()
        {
            yield return new WaitForSeconds(hitboxDelay);
            hitbox = CreateHitbox(hitboxInitiallyOn, damage, knockback, damageSelf, priority, rehitTime);
            hitbox.hitData.hitBehavior.shieldDamage = 0.7f;
            FollowObject(hitbox.transform, hitboxAttachmentPoint, hitboxOffset);

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