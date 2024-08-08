using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Logics.Displaying.MainMenu
{
    public class ArtifactDescriptionWidget : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private Sprite[] _artifactRareSprite;
        [SerializeField] private Color[] _artifactTextColors;

        [Header("UI References")]
        [SerializeField] private Image _rareImage;
        [SerializeField] private TMP_Text _artifactNameText;
        [SerializeField] private TMP_Text _artifactPlanetNameText;
        [SerializeField] private Image _artifactDisplayImage;
        [SerializeField] private TMP_Text _artifactPassiveBonusText;
        [SerializeField] private TMP_Text _artifactActiveBonusText;

        public void SetRareWidget(int rare)
        {
            _rareImage.sprite = _artifactRareSprite[0];
            _artifactNameText.color = _artifactTextColors[rare];
            _artifactPlanetNameText.color= _artifactTextColors[rare];
        }

        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);
    }
}