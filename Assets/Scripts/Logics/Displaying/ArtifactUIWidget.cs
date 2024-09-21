using Game.Types;
using Services.Events;
using Services.locator;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Logics.Displaying
{
    public class ArtifactUIWidget : MonoBehaviour, IPointerClickHandler, InjectDependency
    {
        public bool IsEmpty { get; private set; } = true;
        public bool IsCooldownState { get; private set; }

        [SerializeField] private Image _displayImage;
        [SerializeField] private Image _rareImage;
        [SerializeField] private Image _cooldownImage;
        [SerializeField] private TMP_Text _cooldownText;
        [SerializeField] private EArtifactRare[] _slotRare;
        [SerializeField] private EArtifactSlotUI _artifactSlotType;

        private IArtifactCastEventsUIExec _artifactCastEventsExec;

        public void Inject(IServicesLocator locator)
        {
            _artifactCastEventsExec = locator.GetServices<IArtifactCastEventsUIExec>();
            Hide();
        }

        public EArtifactRare[] GetSlotRare() => _slotRare;
        public EArtifactSlotUI GetArtifactSlotType() => _artifactSlotType;

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);

        public void SetNotEmptySlot()
        {
            IsEmpty = false;
            _cooldownImage.fillAmount = 0.0f;
            _cooldownText.text = string.Empty;
        }

        public void SetDisplayImage(Sprite displayImage) => _displayImage.sprite = displayImage;
        public void SetRareImage(Sprite rareImage) => _displayImage.sprite = rareImage;
        public void UpdateCooldownProgress(float currentTimer, float maxTimer)
        {
            IsCooldownState = true;

            _cooldownImage.fillAmount = currentTimer / maxTimer;
            _cooldownText.text = Mathf.RoundToInt(currentTimer).ToString();

            if (currentTimer <= 0.0f)
            {
                _cooldownImage.fillAmount = 0.0f;
                _cooldownText.text = string.Empty;
                IsCooldownState = false;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (IsCooldownState == false)
            {
                _artifactCastEventsExec.OnArtifactSlotClicked(_artifactSlotType);
                IsCooldownState = true;
            }
        }

    }
}