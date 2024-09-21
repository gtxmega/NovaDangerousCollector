using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Logics.Displaying.MainMenu
{
    public class WeaponSelectWidget : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public bool IsEmpty { get; private set; } = true;
        public int WeaponID { get; private set; } = -1;

        [SerializeField] private Sprite _emptySlotSprite;

        [Header("UI")]
        [SerializeField] private Image _weaponImage;
        [SerializeField] private Image _weaponRareImage;

        [Header("Animations")]
        [SerializeField] private Ease _setDisplayImageEase;
        [SerializeField] private float _setDisplayImageMultiplyScale;
        [SerializeField] private float _setDisplayImageDuration;

        private WeaponCollection _weaponCollection;
        private int _widgetID;

        public void SetWeaponCollection(WeaponCollection weaponCollection) => _weaponCollection = weaponCollection;
        public void SetWidgetID(int widgetID) => _widgetID = widgetID;
        public void SetRareImage(Sprite sprite)
        {
            _weaponRareImage.color = Color.white;
            _weaponRareImage.sprite = sprite;
        }

        public void SetDisplayImage(Sprite sprite)
        {
            _weaponImage.sprite = sprite;
            _weaponImage.enabled = true;

            _weaponImage
                    .rectTransform.DOPunchScale(Vector3.one * _setDisplayImageMultiplyScale, _setDisplayImageDuration)
                    .SetEase(_setDisplayImageEase)
                    .SetLink(_weaponImage.gameObject);
        }

        public void SetWeapon(int weaponID)
        {
            WeaponID = weaponID;
            IsEmpty = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (IsEmpty == false)
                _weaponCollection.OnClickWidget(WeaponID);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _weaponCollection.OnEnterSelectedWidget(_widgetID);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _weaponCollection.OnExitSelectedWidget(_widgetID);
        }
    }
}