using Pratfall.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{
    /// <summary>
    /// Controls the camera's tracking to keep players within view
    /// </summary>
    [RequireComponent(typeof(ObjectTracker))]
    public class TrackPlayers : MonoBehaviour
    {
        private ObjectTracker tracker;
        // Start is called before the first frame update
        void Start()
        {
            tracker = GetComponent<ObjectTracker>();
        }

        void OnEnable()
        {
            RoundLogic.RoundStarted += RoundLogic_RoundStarted;
        }

        void OnDisable()
        {
            RoundLogic.RoundStarted -= RoundLogic_RoundStarted;
        }

        void RoundLogic_RoundStarted()
        {
            List<Transform> transforms = new List<Transform>();
            foreach(BaseCharacter c in RoundLogic.spawnedCharacters)
            {
                transforms.Add(c.worldCollider.transform);
            }
            tracker.tracked = transforms.ToArray();
        }
    }
}