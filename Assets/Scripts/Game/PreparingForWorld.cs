using Logics.Displaying.MainMenu;
using Services.Events;
using Services.locator;
using UnityEngine;

namespace Game
{
    public class PreparingForWorld : MonoBehaviour, InjectDependency
    {
        [SerializeField] private ArtifactsCollection _artifactsCollection;
        [SerializeField] private WeaponCollection _weaponCollection;
        
        private GameInstance _gameInstance;
        private IMenuEvents _menuEvents;
        private AccoutrementsPlayer _accoutrementsPlayer;

        public void Inject(IServicesLocator locator)
        {
            _gameInstance = locator.GetServices<GameInstance>();
            _menuEvents = locator.GetServices<IMenuEvents>();
            _accoutrementsPlayer = locator.GetServices<AccoutrementsPlayer>();

            _menuEvents.PlayerStartWorld += OnPlayerStartWorld;
        }

        private void OnPlayerStartWorld(int levelIndex)
        {
            _accoutrementsPlayer.SetArtifacts(_artifactsCollection.GetSelectedArtifacts());
            _accoutrementsPlayer.SetWeapons(_weaponCollection.GetSelectedWeapons());
            _accoutrementsPlayer.SetCurrentWorldIndex(levelIndex);

            _gameInstance.LoadScene(levelIndex);
        }

        private void OnDestroy()
        {
            _menuEvents.PlayerStartWorld -= OnPlayerStartWorld;
        }
    }
}