using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Logics.Displaying.MainMenu
{
    public class DropLootWidget : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _rareImage;
        [SerializeField] private Image _displayImage;

        public void SetRareImage(Sprite rareSprite) => _rareImage.sprite = rareSprite;
        public void SetDisplayImage(Sprite displayImage) => _displayImage.sprite = displayImage;

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            
        }
    }
}