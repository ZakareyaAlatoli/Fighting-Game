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
        private TextMeshProUGUI text;

        public class MissingHUDElementException : System.Exception { }

        // Start is called before the first frame update
        void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
            if(displayable == null)
                text.text = "NULL";
            else
                displayable.HUDStringChanged += UpdateString;
        }

        void OnValidate()
        {
            if (text == null)
                text = GetComponent<TextMeshProUGUI>();
            //If the HUD Element was changed...
            if(displayable != previousDisplayable)
            {
                if(previousDisplayable != null)
                    previousDisplayable.HUDStringChanged -= UpdateString;

                if(displayable != null)
                {
                    displayable.HUDStringChanged += UpdateString;
                    text.text = displayable.hudString.GetDisplayableValue();
                }
                previousDisplayable = displayable;
            }
        }

        void UpdateString(string newString)
        {
            text.text = displayable.hudString.GetDisplayableValue();
        }
    }
}