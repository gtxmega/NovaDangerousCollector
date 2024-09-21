using Logics.Actors;
using Services.locator;
using TMPro;
using UnityEngine;

namespace Logics.Displaying.MainMenu
{
    public class PlayerAttributesDisplaying : MonoBehaviour, InjectDependency
    {
        [SerializeField] private PlayerActorConfig _playerActorConfig;
        [SerializeField] private TMP_Text _attributeText;

        public void Inject(IServicesLocator locator)
        {
            FillAttributeText();
        }

        private void FillAttributeText()
        {
            _attributeText.text = _playerActorConfig.GetAttributesText();
        }
    }
}