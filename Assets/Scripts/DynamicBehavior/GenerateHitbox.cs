using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall.MoveBehaviors
{
    [System.Serializable]
    public struct HitboxAttachment
    {
        public GameObject hitbox;
        public Transform attachmentPoint;
        public Vector3 offset;
        public Vector3 rotation;

        public Hitbox[] hitboxData { get; set; }
    }


    public class GenerateHitbox : DynamicBehavior
    {
        void Awake()
        {
            hitboxInstances = new List<GameObject>();
        }
        public GameObject hitboxMaster { get; private set; }
        public HitboxLayer hitboxLayer { get; private set; }

        public HitboxAttachment[] hitboxes;
        public List<GameObject> hitboxInstances { get; private set; }

        public GameObject hitboxOrigin;
        public Characters.BaseCharacter character;

        public override void Perform()
        {
            hitboxMaster = new GameObject("HitboxLayer");
            hitboxLayer = hitboxMaster.AddComponent<HitboxLayer>();
            for(int i=0; i<hitboxes.Length; i++)
            {
                GameObject instance = Instantiate(hitboxes[i].hitbox);
                instance.SetActive(false);
                hitboxInstances.Add(instance);
                hitboxes[i].hitboxData = instance.GetComponentsInChildren<Hitbox>();

                foreach (Hitbox h in hitboxes[i].hitboxData)
                {
                    if(!character.facingRight)
                        h.hitData.hitBehavior.knockback.x *= -1f;
                    h.hitData.hitTags.origin = hitboxOrigin;
                    h.hitData.hitTags.team = character.team;

                    h.hitData.layer = hitboxLayer;
                    hitboxLayer.hitboxes.Add(h);
                }
                HitboxAttachment attach = hitboxes[i];
                if(attach.attachmentPoint != null)
                {
                    StartCoroutine(MaintainHitboxPosition(instance, attach.attachmentPoint, attach.offset, attach.rotation));
                }
            }
        }

        IEnumerator MaintainHitboxPosition(GameObject hitbox, Transform parent, Vector3 offset, Vector3 rotation)
        {
            while(hitbox != null)
            {
                hitbox.transform.rotation = parent.rotation;
                hitbox.transform.position = parent.position + offset;
                yield return null;
            }
        }
    }
}