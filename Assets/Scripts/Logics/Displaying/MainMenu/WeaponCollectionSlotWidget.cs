using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Logics.Displaying.MainMenu
{
    public class WeaponCollectionSlotWidget : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Header("UI References")]
        [SerializeField] private TMP_Text _weaponNameText;
        [SerializeField] private Image _weaponDisplayImage;
        [SerializeField] private Image _rareImage;
        [SerializeField] private GameObject _lockObject;
        [SerializeField] private GameObject _selectedText;

        private int _weaponID = -1;

        private WeaponCollection _weaponCollection;

        public void SetDisplayImage(Sprite displayImage) => _weaponDisplayImage.sprite = displayImage;
        public void SetWeaponCollection(WeaponCollection weaponCollection) => _weaponCollection = weaponCollection;
        public void SetWeaponID(int weaponID) => _weaponID = weaponID;
        public void SetRareImage(Sprite sprite) => _rareImage.sprite = sprite;
        public void SetRareColor(Color rareColor) => _weaponNameText.color = rareColor;
        public void SetWeaponName(string name) => _weaponNameText.text = name;

        public void Unlock()
        {
            _lockObject.SetActive(false);
        }

        public void Lock()
        {
            _lockObject.SetActive(true);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _weaponCollection.OnClickWidget(_weaponID);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _weaponCollection.OnDragWidget(_weaponID);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _weaponCollection.OnBeginDragWidget(_weaponID);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if(_weaponCollection.OnEndDragWidget(_weaponID))
            {
                _selectedText.SetActive(true);
            }
        }
    }
}