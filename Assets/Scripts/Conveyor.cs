using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{
    public class Conveyor : MonoBehaviour
    {
        public Collider objectToMatchSpeed;
        private List<Collider> objectsOnTop;
        // Start is called before the first frame update
        void Awake()
        {
            objectsOnTop = new List<Collider>();
            Physics.IgnoreCollision(objectToMatchSpeed.GetComponent<Collider>(), GetComponent<Collider>(), true);
        }

        void Start()
        {
            StartCoroutine(ClearAttachedRigidbody());
        }

        IEnumerator ClearAttachedRigidbody()
        {
            yield return new WaitForEndOfFrame();
            objectsOnTop.Clear();
        }

        void OnTriggerEnter(Collider other)
        {
            objectsOnTop.Add(other);
        }


        void OnTriggerExit(Collider other)
        {
            objectsOnTop.Remove(other);
        }

        Vector3 previousPosition;
        void FixedUpdate()
        {    
            //The velocity of the conveyor will be applied to all objects on top of it
            transform.position = objectToMatchSpeed.transform.position;
            Vector3 velocity = (transform.position - previousPosition);

            previousPosition = transform.position;
            transform.rotation = objectToMatchSpeed.transform.rotation;
            transform.position += new Vector3(0f, transform.lossyScale.y / 2.0f, 0f);
            foreach(Collider c in objectsOnTop)
            {
                c.transform.Translate(velocity, Space.World);
            }
        }
    }
}