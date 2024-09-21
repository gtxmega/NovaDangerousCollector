using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Logics.Displaying.MainMenu
{
    public class ArtifactDescriptionWidget : MonoBehaviour
    {
        public event Action ClickStartWorld;

        [Header("UI References")]
        [SerializeField] private Image _rareImage;
        [SerializeField] private TMP_Text _artifactNameText;
        [SerializeField] private TMP_Text _artifactPlanetNameText;
        [SerializeField] private Image _artifactDisplayImage;
        [SerializeField] private TMP_Text _artifactPassiveBonusText;
        [SerializeField] private TMP_Text _artifactActiveBonusText;
        [SerializeField] private Canvas _rootCanvas;

        [Header("World info references")]
        [SerializeField] private GameObject _startWorldButton;
        [SerializeField] private GameObject _worldAttributesPanel;
        [SerializeField] private DropsLootPanel _artifactsDropsPanel;
        [SerializeField] private DropsLootPanel _weaponsDropsPanel;
        [SerializeField] private DropsLootPanel _populationPanel;


        public void SetRareImage(Sprite spriteRare) => _rareImage.sprite = spriteRare;
        public void SetRareColor(Color colorRare)
        {
            _artifactNameText.color = colorRare;
            _artifactPlanetNameText.color = colorRare;
        }

        public void SetName(string name) => _artifactNameText.text = name;

        #region WorldCollection

        public void ShowWorldButton() => _startWorldButton.SetActive(true);
        public void HideWorldButton() => _startWorldButton.SetActive(false);

        public DropLootWidget[] GetArtifactDropWidgets() => _artifactsDropsPanel.GetWidgets;
        public void ShowArtifactsDropsPanel(int countWidgets) => _artifactsDropsPanel.Show(countWidgets);
        public void HideArtifactsDropsPanel() => _artifactsDropsPanel.Hide();

        public DropLootWidget[] GetWeaponDropWidgets() => _weaponsDropsPanel.GetWidgets;
        public void ShowWeaponDropsPanel(int countWidgets) => _weaponsDropsPanel.Show(countWidgets);
        public void HideWeaponsDropsPanel() => _weaponsDropsPanel.Hide();

        public DropLootWidget[] GetPopulationDropWidgets() => _populationPanel.GetWidgets;
        public void ShowPopulationPanel(int countWidgets) => _populationPanel.Show(countWidgets);
        public void HidePopulationPanel() => _populationPanel.Hide();

        public void OnClickStartWorld() => ClickStartWorld?.Invoke();

        #endregion

        public void SetPlanetName(string planetName)
        {
            if (planetName != string.Empty)
                _artifactPlanetNameText.text = "from planet " + planetName;
            else
                _artifactPlanetNameText.text = string.Empty;
        }

        public void SetDisplayImage(Sprite displayImage) => _artifactDisplayImage.sprite = displayImage;
        
        public void SetPassiveBonusText(string passBonusText)
        {
            _artifactPassiveBonusText.text = passBonusText;
        }

        public void SetActiveBonusText(string activeBonusText)
        {
            _artifactActiveBonusText.text = activeBonusText;
        }

        public void ShowArtifact()
        {
            _worldAttributesPanel.SetActive(false);
            HideArtifactsDropsPanel();
            HideWeaponsDropsPanel();
            HidePopulationPanel();
            HideWorldButton();

            _artifactPlanetNameText.gameObject.SetActive(true);
            _artifactPassiveBonusText.gameObject.SetActive(true);
            _artifactActiveBonusText.gameObject.SetActive(true);
            _rootCanvas.enabled = true;
        }

        public void ShowWorld()
        {
            _artifactPlanetNameText.gameObject.SetActive(false);
            _artifactActiveBonusText.gameObject.SetActive(false);
            _artifactPassiveBonusText.gameObject.SetActive(false);
            _worldAttributesPanel.SetActive(true);
            _rootCanvas.enabled = true;
        }

        public void Hide()
        {
            _rootCanvas.enabled = false;
        }
    }
}