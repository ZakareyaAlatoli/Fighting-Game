using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

namespace Pratfall.UI
{
    /// <summary>
    /// Updates the attached Text component's text to match the value of another component's public field
    /// </summary>
    [ExecuteInEditMode][RequireComponent(typeof(Text))]
    public class DisplayField : MonoBehaviour
    {
        public int selectedIndex;

        private FieldInfo[] fields;
        public Object component;
        private List<string> fieldNames;
        private Object previousComponent;
        private Text text;

        // Start is called before the first frame update
        void Awake()
        {
            text = GetComponent<Text>();
            previousComponent = component;
        }
        void OnValidate()
        {
            if (component != previousComponent)
            {
                fields = component.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
                fieldNames = new List<string>();
                foreach(FieldInfo f in fields)
                {
                    fieldNames.Add(f.Name);
                }
                previousComponent = component;
            }

            if (fields != null && fields.Length >= 1)
            {
                if (selectedIndex < 0 || selectedIndex >= fields.Length)
                    selectedIndex = 0;
                if (text == null)
                    text = GetComponent<Text>();
                text.text = fields[selectedIndex].GetValue(component).ToString();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if(fields != null && fields.Length >= 1)
            {
                if(selectedIndex < 0 || selectedIndex >= fields.Length)
                    selectedIndex = 0;
                text.text = fields[selectedIndex].GetValue(component).ToString();
            }       
        }
    }
}