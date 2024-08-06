using UnityEngine;

namespace Services.InputHandler
{
    public class MobileInput : MonoBehaviour, IInputHandler
    {
        [SerializeField] private Joystick _joystick;

        public float GetHorizontal() => _joystick.Horizontal;

        public float GetVertical() => _joystick.Vertical;
    }
}