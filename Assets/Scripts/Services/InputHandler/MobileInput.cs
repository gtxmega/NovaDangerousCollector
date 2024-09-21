using UnityEngine;

namespace Services.InputHandler
{
    public class MobileInput : MonoBehaviour, IInputHandler
    {
        [SerializeField] private Joystick _joystick;

        public float GetHorizontal() => _joystick.Horizontal;

        public float GetVertical() => _joystick.Vertical;

        public Vector3 GetMousePosition()
        {
            if(Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                return touch.position;
            }

            return Vector3.zero;
        }
    }
}