using Levels;
using Logics.Views;
using Services.Events;
using Services.locator;
using UnityEngine;

namespace Logics.Installer
{
    public class WeaponInstaller : MonoBehaviour, InjectDependency
    {
        private LevelState _levelState;
        private ILevelEvents _levelEvents;

        public void Inject(IServicesLocator locator)
        {
            _levelState = locator.GetServices<LevelState>();
            _levelEvents = locator.GetServices<ILevelEvents>();

            _levelEvents.PlayerSpawnActor += OnPlayerSpawnActor;
        }

        private void OnPlayerSpawnActor(PlayerView actorView)
        {

        }

        private void OnDestroy()
        {
            if (_levelState != null)
            {
                _levelEvents.PlayerSpawnActor -= OnPlayerSpawnActor;
            }
        }
    }
}