using System;
using UnityEngine;
using UnityEngine.Events;

namespace UnityStandardAssets.CrossPlatformInput
{
    [Serializable]
    public class ButtonPressEvent : UnityEvent { }
    public class ButtonHandler : MonoBehaviour
    {

        public ButtonPressEvent buttonPressEvent = new ButtonPressEvent();
        public string Name;
        bool pointerDown = false;
        void OnEnable()
        {

        }

        void Update()
        {
            if (pointerDown)
                buttonPressEvent.Invoke();
        }
        public void OnPointerDown()
        {
            pointerDown = true;
        }

        public void OnPointerUp()
        {
            pointerDown = false;
        }

        public void SetDownState()
        {
            CrossPlatformInputManager.SetButtonDown(Name);
        }

        public void SetUpState()
        {
            CrossPlatformInputManager.SetButtonUp(Name);
        }

        public void SetAxisPositiveState()
        {
            CrossPlatformInputManager.SetAxisPositive(Name);
        }

        public void SetAxisNeutralState()
        {
            CrossPlatformInputManager.SetAxisZero(Name);
        }

        public void SetAxisNegativeState()
        {
            CrossPlatformInputManager.SetAxisNegative(Name);
        }
    }
}