using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Pratfall.Input
{
    public interface ISelectableByPlayer
    {
        void OnSelectedByPlayer(InputHandler player);
    }
    /// <summary>
    /// Floating player-specific cursor
    /// </summary>
    public class Cursor : MonoBehaviour, IControllable
    {
        [SerializeField]
        private Text _text;
        GraphicRaycaster m_Raycaster;
        PointerEventData m_PointerEventData;
        EventSystem m_EventSystem;
        /// <summary>
        /// Displays which player this cursor belongs to
        /// </summary>
        public string header
        {
            get
            {
                return _text.text;
            }
            set
            {
                _text.text = value;
            }
        }

        [HideInInspector]
        public InputHandler associatedPlayer;
        public float speed = 7.5f;

        // Start is called before the first frame update
        void Start()
        {
            _text = GetComponent<Text>();
            //Fetch the Raycaster from the GameObject (the Canvas)
            m_Raycaster = FindObjectOfType<Canvas>().GetComponent<GraphicRaycaster>();
            //Fetch the Event System from the Scene
            m_EventSystem = EventSystem.current;
        }

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
        }

        void Update()
        {
            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x, 0f, Screen.width);
            pos.y = Mathf.Clamp(pos.y, 0f, Screen.height);
            transform.position = pos;
        }

        void Select()
        {
            //TAKEN FROM UNITY DOCS
            //Set up the new Pointer Event
            m_PointerEventData = new PointerEventData(m_EventSystem);
            //Set the Pointer Event Position to that of the mouse position
            m_PointerEventData.position = transform.position;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            m_Raycaster.Raycast(m_PointerEventData, results);

            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                IPointerClickHandler[] clickHandler = result.gameObject.GetComponents<IPointerClickHandler>();
                ISelectableByPlayer[] playerSelector = result.gameObject.GetComponents<ISelectableByPlayer>();
                if (clickHandler != null)
                {
                    foreach(IPointerClickHandler pch in clickHandler)
                        pch.OnPointerClick(m_PointerEventData);
                }
                if (playerSelector != null)
                {
                    foreach (ISelectableByPlayer sbp in playerSelector)
                        sbp.OnSelectedByPlayer(associatedPlayer);
                }
            }
        }
    }
}