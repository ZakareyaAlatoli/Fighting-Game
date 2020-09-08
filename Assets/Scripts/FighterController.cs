using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pratfall
{
    public class FighterController : MonoBehaviour
    {
        private int STANDARD_ATTACK;
        public Animator animator;
        public Hitbox punchHitbox;

        // Start is called before the first frame update
        void Start()
        {
            STANDARD_ATTACK = Animator.StringToHash("StandardAttack");
        }

        //--MOVE DEFINITIONS--//
        private IEnumerator MV_StandardAttack()
        {
            int frameCount = 0;
            punchHitbox.gameObject.SetActive(true);
            animator.Play(STANDARD_ATTACK);
            while(frameCount < 20)
            {
                frameCount++;
                yield return null;
            }
            punchHitbox.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                StopCoroutine(MV_StandardAttack());
                StartCoroutine(MV_StandardAttack());
            }
        }
    }
}