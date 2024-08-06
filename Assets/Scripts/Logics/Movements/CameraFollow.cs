using Logics.Views;
using Services.Events;
using Services.locator;
using UnityEngine;

namespace Logics.Movements
{
    public class CameraFollow : MonoBehaviour, InjectDependency
    {
        [SerializeField] private Transform _selfTransform;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _smoothSpeed;

        private ILevelEvents _levelEvents;
        private Transform _playerTransform;

        public void Inject(IServicesLocator locator)
        {
            _levelEvents = locator.GetServices<ILevelEvents>();

            _levelEvents.PlayerSpawnActor += OnPlayerSpawnActor;
        }

        private void OnPlayerSpawnActor(PlayerView playerView)
        {
            _playerTransform = playerView.SelfTransform;
        }

        private void FixedUpdate()
        {
            if (_playerTransform != null)
            {
                Vector3 destination = _playerTransform.position + _offset;
                _selfTransform.position = Vector3.Lerp(_selfTransform.position, destination, _smoothSpeed);
            }
        }

        private void OnDestroy()
        {
            if (_levelEvents != null)
            {
                _levelEvents.PlayerSpawnActor -= OnPlayerSpawnActor;
            }
        }
    }
}