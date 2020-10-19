using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall.Characters.StandardCharacterMoves
{
    [AddComponentMenu("Pratfall/Attacks/StandardCharacter/JAB")]
    public class atk_StandardChar_JAB : BaseAttack
    {
        [Header("Plug-in Dependencies")]
        public Transform hitboxAttachment;
        //Private Resources
        
        //MOVE BEHAVIOR
        protected override IEnumerator Behavior()
        {
            yield break;
        }
        //END MOVE BEHAVIOR



        protected override void OnInterrupted() { }

        protected override void OnFinished() { }


        //Helper functions
        GameObject CreateHitbox()
        {
            GameObject hitbox = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //DO STUFF WITH HITBOX

            //-------------------
            return hitbox;
        }

        Coroutine FollowObject(Transform follower, Transform followee)
        {
            return StartCoroutine(CR_FollowObject(follower, followee));
        }

        IEnumerator CR_FollowObject(Transform follower, Transform followee)
        {
            while(follower != null && followee != null)
            {
                follower.position = followee.position;
                yield return null;
            }
        }
    }
}