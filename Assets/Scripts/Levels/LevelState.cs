using Leopotam.Ecs;
using Logics.Artifacts;
using Logics.Views;
using Services.Factory;
using Services.Factory.Builders;
using Services.locator;
using UnityEngine;

namespace Levels
{
    public class LevelState : MonoBehaviour, InjectDependency
    {
        [Header("Weapons")]
        [SerializeField] private WeaponConfig[] _defaultWeaponConfigs;

        [Header("Artifacts")]
        [SerializeField] private ArtifactConfig _artifactConfig;

        private static LevelState _instance;

        private IActorFactory _actorFactory;

        public void Inject(IServicesLocator locator)
        {
            _actorFactory = locator.GetServices<IActorFactory>();
        }

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void TryConfigurePlayerEntity(PlayerView playerView)
        {
            ref EcsEntity playerEntity = ref playerView.GetEntity();

            ref EcsEntity artifactEntity = ref _actorFactory.CreateArtifactEntity(_artifactConfig, playerEntity);

            for (int i = 0; i < _defaultWeaponConfigs.Length; ++i)
            {
                Transform weaponSocket = playerView.GetFreeWeaponSocket();
                if (weaponSocket != null)
                {
                    ref EcsEntity weaponEntity = ref _actorFactory.CreateWeaponEntity(_defaultWeaponConfigs[i], in playerEntity, weaponSocket);
                }
            }
        }
    }
}