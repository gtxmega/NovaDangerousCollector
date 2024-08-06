using UnityEngine;

namespace Logics.Artifacts
{
    [CreateAssetMenu(menuName = "Game/Artifacts/The Emperor's eye", fileName = "ArtifactEmperorEyeConfig")]
    public class ArtifactEmperorEyeConfig : ArtifactConfig
    {
        [field: Header("Artifact main attributes")]
        [field: SerializeField][field: Range(0.0f, 1.0f)] public float EnemyDamageReduce { get; private set; }
        [field: SerializeField][field: Range(0.0f, 1.0f)] public float EnemyMoveSpeedReduce { get; private set; }
        [field: SerializeField][field: Range(0.0f, 1.0f)] public float EnemyAllResistanceReduce { get; private set; }

        public override Artifact GetArtifactInstance()
        {
            return new ArtifactEmperorEye();
        }

        public override ArtifactBuilder GetBuilder()
        {
            return new ArtifactEmperorEyeBuilder(this);
        }
    }
}