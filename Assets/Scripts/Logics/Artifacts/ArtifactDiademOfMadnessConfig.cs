using UnityEngine;

namespace Logics.Artifacts
{
    [CreateAssetMenu(menuName = "Game/Artifacts/DiademOfMadness", fileName = "DiademOfMadnessConfig")]
    public class ArtifactDiademOfMadnessConfig : ArtifactConfig
    {
        [field: Header("Passive bonus")]
        [field: SerializeField] public float PassiveDecreaseHealth { get; private set; }
        [field: SerializeField][field: Tooltip("Percent value")] public float RestoreHealthPerKill { get; private set; }

        [field: Header("Active bonus")]
        [field: SerializeField] public float BonusAttackSpeed { get; private set; }
        [field: SerializeField] public float BonusMoveSpeed { get; private set; }
        [field: SerializeField] public float MultiplierPassiveBonus { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }

        [field: Header("Cooldown")]
        [field: SerializeField] public float CooldownTime { get; private set; }

        public override Artifact GetArtifactInstance()
        {
            return new ArtifactDiademOfMadness();
        }
        public override ArtifactBuilder GetBuilder()
        {
            return new ArtifactDiademOfMadnessBuilder(this);
        }
    }
}