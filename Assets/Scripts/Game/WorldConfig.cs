using Game.Types;
using Logics.Actors;
using Logics.Artifacts;
using Services.Factory.Builders;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Game/Worlds/BaseWorld", fileName = "WorldConfig")]
    public class WorldConfig : ScriptableObject
    {
        [field: SerializeField] public string WorldName { get; private set; }
        [field: SerializeField] public Sprite DisplayImage { get; private set; }
        [field: SerializeField] public EArtifactRare WorldRare { get; private set; }
        [field: SerializeField] public int SceneID { get; private set; }
        [field: SerializeField] public NpcActorConfig[] NpcActorConfigs { get; private set; } = new NpcActorConfig[0];
        [field: SerializeField] public ArtifactConfig[] ArtifactConfigs { get; private set; } = new ArtifactConfig[0];
        [field: SerializeField] public WeaponConfig[] WeaponConfigs { get; private set; } = new WeaponConfig[0];
    }
}