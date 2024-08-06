using UnityEngine;
using UnityEngine.UI;

namespace Logics.Displaying
{
    public class HealthIndicator : MonoBehaviour
    {
        [SerializeField] private RectTransform _rootRect;
        [SerializeField] private Image _colorImage;

        public void Show() => gameObject.SetActive(true);

        public void UpdateHealth(float current, float max)
        {
            _colorImage.fillAmount = current / max;
        }

        public void SetPosition(Vector3 position) => _rootRect.position = position;

        public void Hide() => gameObject.SetActive(false);
    }
}