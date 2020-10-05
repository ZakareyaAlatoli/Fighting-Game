using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall.Input
{
    public class Cursor : MonoBehaviour, IControllable
    {
        public float speed = 5f;
        public void OnAttack(Vector2 direction)
        {
            
        }

        public void OnBlock()
        {
            //throw new System.NotImplementedException();
        }

        public void OnJump()
        {
            Select();
        }

        public void OnMove(Vector2 direction)
        {
            transform.Translate(direction * speed);
            //TO DO: Prevent going off screen
        }

        void Select()
        {
            //TO DO: Raycast from the cursor position to select objects on screen
        }

        // Start is called before the first frame update
        void Start() { }
    }
}