using UnityEngine;

namespace Services.InputHandler
{
    public interface IInputHandler
    {
        float GetHorizontal();
        Vector3 GetMousePosition();
        float GetVertical();
    }
}