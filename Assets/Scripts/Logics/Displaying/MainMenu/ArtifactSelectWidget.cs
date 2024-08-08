using Game.Types;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Logics.Displaying.MainMenu
{
    public class ArtifactSelectWidget : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public bool IsEmpty { get; private set; } = true;
        public int ArtifactID => _artifactId;

        [SerializeField] private EArtifactRare[] _neededRares;

        [SerializeField] private Sprite[] _entityRareSprite;
        [SerializeField] private Sprite _emptySlotSprite;

        [Header("UI References")]
        [SerializeField] private Image _entityDisplayImage;
        [SerializeField] private Image _entityRareImage;

        private int _artifactId;
        private int _widgetId;
        private ArtifactsCollection _artifactsCollection;

        public void SetArtifactCollection(ArtifactsCollection collection) => _artifactsCollection = collection;
        public void SetWidgetID(int widgetID) => _widgetId = widgetID;
        public void SetSlotRare(int rare) => _entityRareImage.sprite = _entityRareSprite[rare];
        
        public void SetDisplayImage(Sprite displaySprite)
        {
            _entityDisplayImage.sprite = displaySprite;
            _entityDisplayImage.enabled = true;
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
    }
}