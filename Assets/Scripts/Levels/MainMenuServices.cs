using Game;
using Services.Events;
using Services.InputHandler;
using Services.locator;
using UnityEngine;

namespace Levels
{
    public class MainMenuServices : MonoBehaviour
    {
        [SerializeField] private MobileInput _mobileInput;

        private ServicesLocator _locator;
        private MenuEvents _menuEvents;
        private AccoutrementsPlayer _accoutrementsPlayer;

        public void Init(GameInstance gameInstance)
        {
            _locator = new();

            _menuEvents = new MenuEvents();
            _accoutrementsPlayer = FindObjectOfType<AccoutrementsPlayer>();

            _locator
                .Registration<GameInstance>(gameInstance)
                .Registration<GameConstants>(gameInstance.GameConstants)
                .Registration<IMenuEvents>(_menuEvents)
                .Registration<IMenuEventsExec>(_menuEvents)
                .Registration<IInputHandler>(_mobileInput)
                .Registration<AccoutrementsPlayer>(_accoutrementsPlayer);
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