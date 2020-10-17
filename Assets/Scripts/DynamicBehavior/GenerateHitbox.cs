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

                foreach(Hitbox h in hitboxes[i].hitboxData)
                {
                    h.hitData.hitTags.origin = hitboxOrigin;
                    h.hitData.hitTags.team = character.team;

                    h.hitData.layer = hitboxLayer;
                    hitboxLayer.hitboxes.Add(h);
                }
                if(hitboxes[i].attachmentPoint != null)
                {
                    instance.transform.SetParent(hitboxes[i].attachmentPoint);
                    instance.transform.localPosition = hitboxes[i].offset;
                    instance.transform.localRotation = Quaternion.Euler(hitboxes[i].rotation);
                }
            }
        }
    }
}