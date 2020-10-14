using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall.UI
{
    [ExecuteAlways][DisallowMultipleComponent][RequireComponent(typeof(IHUDString))]
    public class HUDString : MonoBehaviour
    {
        public event System.Action<string> HUDStringChanged;
        private string previousString;
        public IHUDString hudString { get; private set; }
        void Awake()
        {
            hudString = GetComponent<IHUDString>();
            if(hudString == null)
            {
                UnityEditor.EditorUtility.DisplayDialog("Missing HUD String Component",
                    "HUDString requires a component that implements IHUDString to function", "Cancel");
                DestroyImmediate(this);
            }
        }

        void OnValidate()
        {
            hudString = GetComponent<IHUDString>();
            if (hudString == null)
                throw new DisplayField.MissingHUDElementException();
        }

        void Update()
        {
            if(previousString != hudString.GetDisplayableValue())
            {
                HUDStringChanged?.Invoke(hudString.GetDisplayableValue());
            }
            previousString = hudString.GetDisplayableValue();
        }
    }
}