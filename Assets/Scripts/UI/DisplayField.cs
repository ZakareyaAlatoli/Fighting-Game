using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using TMPro;

namespace Pratfall.UI
{
    /// <summary>
    /// Components that display a value in the HUD should implement this. 
    /// HUDStringChanged should be invoked when the displayable value changes
    /// </summary>
    public interface IHUDString
    {
        /// <summary>
        /// Invoked when the displayable value is changed
        /// </summary>
        event System.Action<string> HUDStringChanged;
        string GetDisplayableValue();
    }

    /// <summary>
    /// Updates the attached Text component's text to match the value of another component's public field
    /// </summary>
    [ExecuteAlways][RequireComponent(typeof(TextMeshProUGUI))]
    public class DisplayField : MonoBehaviour
    {
        public HUDString displayable;
        //Used to detect when the referenced displayable is changed
        private HUDString previousDisplayable;
        private IHUDString displayString;
        private TextMeshProUGUI text;

        public class MissingHUDElementException : System.Exception { }

        // Start is called before the first frame update
        void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
            if(displayable == null)
                text.text = "NULL";
            else
            {
                displayString = displayable.hudString;
                if (displayString != null)
                {
                    text.text = displayString.GetDisplayableValue();
                    displayString.HUDStringChanged += UpdateString;
                }
            }
        }

        void OnValidate()
        {
            if (text == null)
                text = GetComponent<TextMeshProUGUI>();
            //If the HUD Element was changed...
            if(displayable != previousDisplayable)
            {
                // to another valid object, then stop listening to the previous object's change in state
                if(displayString != null)
                    displayString.HUDStringChanged -= UpdateString;
                // to null...
                if (displayable == null)
                {
                    // and there is no display string
                    if (displayString != null)
                        displayString.HUDStringChanged -= UpdateString;
                    text.text = "NULL";
                }
                // to another valid object...
                else
                {
                    displayString = displayable.hudString;
                    // and that object has retrieved its display string
                    if(displayString != null)
                    {
                        text.text = displayString.GetDisplayableValue();
                        displayString.HUDStringChanged += UpdateString;
                    }
                }
                previousDisplayable = displayable;
            }
        }

        void UpdateString(string newString)
        {
            text.text = displayString.GetDisplayableValue();
        }
    }
}