using Logics.Artifacts;
using Services.Factory.Builders;
using UnityEngine;

namespace Game
{
    public class AccoutrementsPlayer : MonoBehaviour
    {
        private int _currentWorldSceneID;

        private ArtifactConfig[] _artifactConfigs;
        private WeaponConfig[] _weaponConfigs;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void SetCurrentWorldIndex(int currentWorldSceneID) => _currentWorldSceneID = currentWorldSceneID;

        public void SetArtifacts(ArtifactConfig[] artifacts) => _artifactConfigs = artifacts;
        public ArtifactConfig[] GetArtifactConfigs() => _artifactConfigs;

        public void SetWeapons(WeaponConfig[] weapons) => _weaponConfigs = weapons;
        public WeaponConfig[] GetWeaponConfigs() => _weaponConfigs;

    }
}