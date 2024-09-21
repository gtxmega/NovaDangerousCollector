using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Logics.Displaying.MainMenu
{
    public class ArtifactCollectionSlotWidget : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
    {
        [Header("UI References")]
        [SerializeField] private TMP_Text _artifactNameText;
        [SerializeField] private Image _artifactDisplayImage;
        [SerializeField] private Image _rareImage;
        [SerializeField] private GameObject _lockObject;
        [SerializeField] private GameObject _selectedText;

        private int _artifactId;
        private ArtifactsCollection _artifactsCollection;

        public void SetArtifactsCollection(ArtifactsCollection collection) => _artifactsCollection = collection;
        public void SetArtifactID(int artifactId) => _artifactId = artifactId;
        public void SetRareImage(Sprite sprite) => _rareImage.sprite = sprite;
        public void SetRareColor(Color color) => _artifactNameText.color = color;

        public void SetNameText(string name) => _artifactNameText.text = name;
        public void SetDisplayImage(Sprite displayImage) => _artifactDisplayImage.sprite = displayImage;
        public void Unlock() => _lockObject.SetActive(false);
        public void Lock() => _lockObject.SetActive(true);


        public void OnBeginDrag(PointerEventData eventData)
        {
            _artifactsCollection.OnBeginDragWidget(_artifactId);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _artifactsCollection.OnDragWidget(_artifactId);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            bool status = _artifactsCollection.OnEndDrag(_artifactId);
            if (status)
            {
                _selectedText.SetActive(true);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _artifactsCollection.OnClickCollectionWidget(_artifactId);
        }
    }
}