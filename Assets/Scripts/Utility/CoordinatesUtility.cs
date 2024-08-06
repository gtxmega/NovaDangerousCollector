using UnityEngine;

namespace Utility
{
    public class CoordinatesUtility : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _cameraTransform;

        private Vector2 _middleScreenPoint;

        private void Awake()
        {
            _middleScreenPoint.x = Screen.width / 2;
            _middleScreenPoint.y = Screen.height / 2;
        }

        public Vector2 GetScreenPosition(Vector3 worldPosition)
        {
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(_camera, worldPosition);
            float distance = Vector3.Distance(_cameraTransform.position, worldPosition);

            float offsetX = _middleScreenPoint.x - screenPoint.x;
            screenPoint.x -= offsetX / distance / Screen.dpi;

            return screenPoint;
        }
    }
}