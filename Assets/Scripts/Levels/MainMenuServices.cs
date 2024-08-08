using Game;
using Services.locator;
using UnityEngine;

namespace Levels
{
    public class MainMenuServices : MonoBehaviour
    {
        private ServicesLocator _locator;

        public void Init(GameInstance gameInstance)
        {
            _locator = new();
        }

        public void InjectToWorldObject()
        {
            foreach (var mono in FindObjectsOfType<MonoBehaviour>())
            {
                if (mono is InjectDependency target)
                    target.Inject(_locator);
            }
        }

        public IServicesLocator GetLocator() => _locator;
    }
}