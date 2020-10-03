using Pratfall.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall.Debugging
{  
    public class DebugControllable : MonoBehaviour, IControllable
    {
        public Rigidbody obj;
        public float speed;

        public void OnAttack(Vector2 direction)
        {

        }

        public void OnBlock()
        {
            throw new System.NotImplementedException();
        }

        public void OnJump()
        {
            throw new System.NotImplementedException();
        }

        public void OnMove(Vector2 direction)
        {
            obj.AddForce(new Vector2(direction.x * speed, 0.0f));
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}