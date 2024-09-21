using UnityEngine;
using UnityEngine.UI;

namespace Logics.Displaying.MainMenu
{
    public class DragWidget : MonoBehaviour
    {
        [SerializeField] private RectTransform _rootRect;
        [SerializeField] private Image _rareImage;
        [SerializeField] private Image _displayImage;

        public void SetRareImage(Sprite sprite) => _rareImage.sprite = sprite;
        public void SetDisplayImage(Sprite displayImage) => _displayImage.sprite = displayImage;
        public void UpdatePosition(Vector3 position) => _rootRect.position = position;

        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);
    }
}