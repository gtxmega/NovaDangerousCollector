using DG.Tweening;
using Game.Types;
using Services.locator;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Logics.Displaying.MainMenu
{
    public class ArtifactSelectWidget : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public bool IsEmpty { get; private set; } = true;
        public int ArtifactID => _artifactId;

        [SerializeField] private EArtifactRare[] _neededRares;

        [SerializeField] private Sprite _emptySlotSprite;

        [Header("UI References")]
        [SerializeField] private Image _entityDisplayImage;
        [SerializeField] private Image _entityRareImage;

        [Header("Animations")]
        [SerializeField] private Ease _setDisplayImageEase;
        [SerializeField] private float _setDisplayImageMultiplyScale;
        [SerializeField] private float _setDisplayImageDuration;

        private int _artifactId;
        private int _widgetId;
        private ArtifactsCollection _artifactsCollection;

        public void SetArtifactCollection(ArtifactsCollection collection) => _artifactsCollection = collection;
        public void SetWidgetID(int widgetID) => _widgetId = widgetID;
        public void SetRareImage(Sprite sprite)
        {
            _entityRareImage.color = Color.white;
            _entityRareImage.sprite = sprite;
        }

        public void SetDisplayImage(Sprite displaySprite)
        {
            _entityDisplayImage.sprite = displaySprite;
            _entityDisplayImage.enabled = true;

            _entityDisplayImage
                .rectTransform.DOPunchScale(Vector3.one * _setDisplayImageMultiplyScale, _setDisplayImageDuration)
                .SetEase(_setDisplayImageEase)
                .SetLink(_entityDisplayImage.gameObject);
        }

        public void SetArtifact(int artifactID)
        {
            _artifactId = artifactID;
            IsEmpty = false;
        }

        public bool CheckSetCondition(EArtifactRare rare)
        {
            for (int i = 0; i < _neededRares.Length; ++i)
            {
                if(_neededRares[i] == rare)
                    return true;
            }

            return false;
        }


        public void ClearSlot()
        {
            _entityDisplayImage.enabled = false;
            _entityRareImage.sprite = _emptySlotSprite;
        }

        public void OnPointerEnter(PointerEventData eventData) => _artifactsCollection.OnEnterSelectWidget(_widgetId);

        public void OnPointerExit(PointerEventData eventData) => _artifactsCollection.OnExitSelectWidget(_widgetId);

        public void OnPointerClick(PointerEventData eventData)
        {
            if(IsEmpty == false)
                _artifactsCollection.OnClickCollectionWidget(_artifactId);
        }
    }
}