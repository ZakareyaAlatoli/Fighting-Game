using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{
    public class LinearMover : MonoBehaviour
    {
        public Vector3 moveVector;
        public Space coordSpace;
        public Transform moveable;
        // Start is called before the first frame update
        void Start()
        {
           
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            moveable.Translate(moveVector, coordSpace);
        }
    }
}